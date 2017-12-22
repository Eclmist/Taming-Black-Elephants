﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Actor2D : MonoBehaviour 
{
    protected Rigidbody2D rigidbody;
    protected Collider2D collider;
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
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        animator = GetComponent<Animator>();

        rigidbody.gravityScale = 0;
        rigidbody.drag = 5;

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

    }

    protected virtual void Move()
    {
        Vector2 movementVector = targetPosition - (Vector2)transform.position;

        if (movementVector.magnitude > movementBias)
        {
            rigidbody.velocity = movementVector.normalized * moveSpeed * internalMoveSpeedMultiplier;
        }
        else
        {
            targetPosition = transform.position;
        }
    }

    public virtual void MoveTo(Vector2 targetPosition, float bias = 0)
    {
        this.targetPosition = targetPosition;
        this.movementBias = bias;
    }

    protected virtual void OnDebug()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveTo(clickPos, 1);
        }
    }
}
