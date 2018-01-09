using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintArrow : MonoBehaviour 
{
    [SerializeField] private float speed = 3;

    
	protected void Update () 
	{
        transform.localPosition = new Vector3(0, 0.5F + (Mathf.Sin(Time.time * speed) + 1) / 10, 0);
	}
}
