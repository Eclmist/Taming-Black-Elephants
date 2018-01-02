using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class Actor2D : MonoBehaviour 
{
    protected Rigidbody2D rb;
    protected Animator animator;

    // Movement
    [SerializeField] [Range(0,2)] protected float moveSpeed = 1;
    protected float internalMoveSpeedMultiplier = 4;

    protected Vector2 targetPosition;
    protected float movementBias;


    // Debug
    [Space(10f)] [SerializeField] protected bool debugMode;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponentInChildren<Animator>();

        rb.gravityScale = 0;
        rb.drag = 5;

        targetPosition = transform.position;
        movementBias = 0.1F;
    }

    protected virtual void Update()
    {
        if (animator != null)
        {
            AnimatorUpdate();
        }

        if (debugMode)
        {
            OnDebug();
        }
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void AnimatorUpdate()
    {
        float horizontal = rb.velocity.x;
        float vertical = rb.velocity.y;

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
            vertical = 0;
        else
            horizontal = 0;

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Magnitude", rb.velocity.sqrMagnitude);


        if (rb.velocity.sqrMagnitude < 0.01F)
            animator.speed = 0;
        else
            animator.speed = 1;
    }

    protected virtual void Move()
    {
        Vector2 movementVector = targetPosition - (Vector2)transform.position;

        if (movementVector.magnitude > movementBias)
        {
            rb.velocity = movementVector.normalized * moveSpeed * internalMoveSpeedMultiplier;
        }
        else
        {
            targetPosition = transform.position;
        }
    }

    public virtual void MoveTo(Vector2 targetPosition, float bias = 0.1F)
    {
        this.targetPosition = targetPosition;
        this.movementBias = bias;
    }

    public virtual void UndoMoveTo()

    {
        this.targetPosition = transform.position;
        this.movementBias = 0.1f;
    }

    protected virtual void OnDebug()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveTo(clickPos);
        }
    }
}
