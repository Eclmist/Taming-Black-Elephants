using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private GameObject knob;
    [SerializeField] private float radius = 75;
    [SerializeField] CanvasGroup cg;

    private Vector2 touchPos;
    private bool showJoystick;

    public Vector2 TouchPosition
    {
        get { return touchPos; } 
    }

    private bool ShowJoystick
    {
        get { return showJoystick; }
        set { showJoystick = value; }
    }

    protected void Update()
    {
        ShowJoystick = true;

        // Initial jump to position
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                transform.position = Input.GetTouch(0).position;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            showJoystick = false;
        }

        // Subsequent drag
        Vector2 knobMovementVector = Vector2.zero;

        if (Input.touchCount > 0 &&
            (Input.GetTouch(0).phase == TouchPhase.Stationary ||
            Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            knob.transform.position = Input.GetTouch(0).position;
            knobMovementVector = knob.transform.position - transform.position;
            showJoystick = true;
        }
        else if (Input.GetMouseButton(0))
        {
            knob.transform.position = Input.mousePosition;
            knobMovementVector = knob.transform.position - transform.position;
            showJoystick = true;
        }

        knobMovementVector = Vector2.ClampMagnitude(knobMovementVector, radius);
        knob.transform.position = transform.position + (Vector3)knobMovementVector;

        touchPos = knobMovementVector / radius;

        if (Time.timeScale == 0)
            showJoystick = false;


        // Fade
        float targetOpacity = showJoystick ? 1 : 0;

        if (Mathf.Abs(cg.alpha - targetOpacity) > 0)
        {
            cg.alpha += (targetOpacity - (showJoystick ? 0 : 1)) * Time.unscaledDeltaTime * 5;
        }
    }

    protected void OnDisable()
    {
        cg.alpha = 0;
    }

}
