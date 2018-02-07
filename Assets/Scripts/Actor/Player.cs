using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Actor2D
{
    //TODO: Place this in some save game manager of sorts
    public static int gameInstance;

    public static Player Instance;
    public bool useJoystick = false;
    public bool allowInteractions = true;

    public bool allowControls = true;

    public float playerReach = 1.5F;

    [SerializeField] private Transform colliderTransform;
    [SerializeField] private AudioClip hitSound;
    private Joystick joystick;

    private static Inventory inventory;

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

        DontDestroyOnLoad(gameObject);

        gameInstance = Random.Range(0, int.MaxValue);
    }

    protected virtual void Start()
    {
        joystick = FindObjectOfType<Joystick>();

        if (inventory == null)
            inventory = GetComponent<Inventory>();
    }

    protected override void Update()
    {
        base.Update();

        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();

            if (joystick != null)
                joystick.gameObject.SetActive(useJoystick);

        }
        else
            joystick.gameObject.SetActive(useJoystick);

        if (!allowControls)
            return;

        Vector2 clickPos = Vector2.zero;
        bool firstTouchFrame = true;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
            !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else if (Input.GetMouseButtonDown(0) && Time.timeScale != 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
            firstTouchFrame = false;

        // Movement
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

        if (firstTouchFrame)
        {
            if (!useJoystick)
                MoveTo(clickPos);

            // Interactions
            if (Time.timeScale == 0 || !allowInteractions)
                return;

            Collider2D[] touchedAreaObjs = Physics2D.OverlapCircleAll(clickPos, 0.01F);

            foreach (Collider2D touched in touchedAreaObjs)
            {
                IInteractable interactable = touched.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if ((transform.position - 
                        (touched.gameObject.transform.position +
                        Vector3.Scale(((Vector3)touched.offset), touched.transform.localScale)))
                        .sqrMagnitude < playerReach)
                    {
                        CheckInteraction(interactable);

                        break;
                    }
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
