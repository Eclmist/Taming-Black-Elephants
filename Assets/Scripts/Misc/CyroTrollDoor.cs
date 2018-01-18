using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyroTrollDoor : MonoBehaviour 
{

    Animator anim;

	protected void Start () 
	{
        anim = GetComponent<Animator>();
        if (!anim)
            Destroy(this);
	}
	
	protected void Update () 
	{
		
	}

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (anim)
            {
                anim.SetTrigger("Close");
            }
        }
    }
}
