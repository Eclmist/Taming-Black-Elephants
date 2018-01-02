using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor2D
{
    public static Player Instance;

    [SerializeField] private Transform colliderTransform;
    private float lastFrameSpeed = 0;

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

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveTo(clickPos);

            if (Time.timeScale == 0)
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
