using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable 
{
    public abstract void Interact();

    protected string hintResourceDir = "Prefab/Hint/";

    protected void Start () 
	{
	}
	
	protected void Update () 
	{
		
	}

    public virtual void Hint()
    {
        DisableHint();

        gameObject.AddComponent<Sprite_Pulse>();

        GameObject hintArrow = Instantiate(Resources.Load(hintResourceDir + "HintArrow")) as GameObject;
        hintArrow.transform.parent = transform;
    }

    public virtual void DisableHint()
    {
        Sprite_Pulse pulseScript = GetComponent<Sprite_Pulse>();

        if (pulseScript != null)
        {
            Destroy(pulseScript);
        }

        GameObject hintArrow = GetComponentInChildren<HintArrow>().gameObject;

        if (hintArrow != null)
        {
            Destroy(hintArrow);
        }
    }
}
