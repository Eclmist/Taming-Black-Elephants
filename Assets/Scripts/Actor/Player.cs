using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor2D
{
    public static Player Instance;
    public bool useJoystick = false;
    public bool allowInteractions = true;

    [SerializeField] private Transform colliderTransform;
    private Joystick joystick;


    public new Transform transform 
    {
        get {return colliderTransform;}
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
    }

    protected override void Update()
    {
        base.Update();

        joystick.gameObject.SetActive(useJoystick);

        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!useJoystick)
            {
                MoveTo(clickPos);
            }

            if (Time.timeScale == 0 || !allowInteractions)
                return;

            Collider2D[] touchedAreaObjs = Physics2D.OverlapCircleAll(clickPos, 0.01F);

            foreach (Collider2D touched in touchedAreaObjs)
            {
                IInteractable interactable = touched.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if ((transform.position - touched.gameObject.transform.position).sqrMagnitude < 1F)
                    {
                        CheckInteraction(interactable);

                        break;
                    }
                }
            }

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
    }

    protected void CheckInteraction(IInteractable queuedInteractableObject)
    {
        if (queuedInteractableObject == null)
            return;

        queuedInteractableObject.Interact();

        UndoMoveTo();

        queuedInteractableObject = null;
    }

}
