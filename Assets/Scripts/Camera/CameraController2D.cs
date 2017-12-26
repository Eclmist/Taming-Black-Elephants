using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController2D : MonoBehaviour {

    //private Camera cam;

    //[SerializeField] private Transform target;

    //[SerializeField] private Rect worldBounds;

    //protected void Start()
    //{
    //    cam = GetComponent<Camera>();

    //}

    //protected void Update ()
    //{
    //    MoveCamera();

    //}

    //private void MoveCamera()
    //{
    //    Rect cameraVisibleBounds = new Rect
    //        (
    //            cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).x, 
    //            cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y,
    //            cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).x,
    //            cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).y - cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y
    //        );

    //    Debug.Log(cameraVisibleBounds);
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(new Vector3(worldBounds.position.x, 0), worldBounds.x + worldBounds.width);

    //}
}
