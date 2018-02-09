using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class OffsetZone : MonoBehaviour
{
    public Vector2 offset;
    public float distanceFromZonePoint;
    CircleCollider2D collider;


    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            distanceFromZonePoint = (collision.transform.position - transform.position).magnitude;
            SmoothFollow.offsetZone = -offset * (Mathf.Max(0,(collider.radius - distanceFromZonePoint)) / collider.radius);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SmoothFollow.offsetZone = Vector2.zero;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) offset);
    }
}
