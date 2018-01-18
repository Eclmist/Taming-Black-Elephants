using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor2D
{
    public static Player Instance;
    public bool useJoystick = false;
    public bool allowInteractions = true;

    public float playerReach = 1.5F;

    [SerializeField] private Transform colliderTransform;
    [SerializeField] private AudioClip hitSound;
    private Joystick joystick;

    private Inventory inventory;

    public new Transform transform 
    {
        get {return colliderTransform;}
    }

    public Inventory Inventory
    {
        get { return inventory; }
    }

    protected override void Awake()
    {
        base.Awake();

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    protected virtual void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        inventory = GetComponent<Inventory>();
    }

    protected override void Update()
    {
        base.Update();

        joystick.gameObject.SetActive(useJoystick);

        Vector2 clickPos = Vector2.zero;
        bool firstTouchFrame = true;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
            firstTouchFrame = false;

        // Movement
        if (firstTouchFrame)
        {
            if (!useJoystick)
                MoveTo(clickPos);
        }

        if (useJoystick)
        {
            if (joystick.TouchPosition.sqrMagnitude < 0.01F)
            {
                UndoMoveTo();
            }
            else
            {
                MoveTo(transform.position + (Vector3)joystick.TouchPosition);
            }
        }


        // Interactions
        if (Time.timeScale == 0 || !allowInteractions)
            return;

        Collider2D[] touchedAreaObjs = Physics2D.OverlapCircleAll(clickPos, 0.01F);

        foreach (Collider2D touched in touchedAreaObjs)
        {
            IInteractable interactable = touched.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if ((transform.position - touched.gameObject.transform.position).sqrMagnitude < playerReach)
                {
                    CheckInteraction(interactable);

                    break;
                }
            }
        }

    }

    protected void CheckInteraction(IInteractable queuedInteractableObject)
    {
        if (queuedInteractableObject == null)
            return;

        queuedInteractableObject.Interact();

        UndoMoveTo();

        queuedInteractableObject = null;
    }

    public void Die()
    {
        transform.parent.transform.position = LevelSetting.Instance.debugSpawnPoint;
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, 2F);
        UndoMoveTo();
    }
}
