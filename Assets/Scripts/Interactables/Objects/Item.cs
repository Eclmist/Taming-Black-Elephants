using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable, IItem
{
    public abstract void Interact();

    [SerializeField] protected Sprite sprite;

    private string name = "Generic Item";

    private string hintResourceDir = "Prefab/Hint/";

    public Sprite Sprite
    {
        get { return sprite; }
    }

    protected void Start () 
	{
	}
	
	protected void Update () 
	{
		
	}

    public string GetName()
    {
        return name;
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

    public void AddToInventory()
    {
        throw new System.NotImplementedException();
    }
}
