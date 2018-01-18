using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IItem
{
    public virtual void Interact()
    {


        AddToInventory();

    }

    [SerializeField] protected Sprite sprite;
    [SerializeField] protected int amount = 1;

    [SerializeField] protected string name = "Generic Item";    // NAME IS UNIQUE ID.
                                                                // Too lazy to write a item ID assignment window

    private string hintResourceDir = "Prefab/Hint/";
    private static string saveDataKey = "null";
    private const string obtainedKey = "obtained";
    private const string inInventory = "inInventory";

    public Sprite Sprite
    {
        get { return sprite; }
    }

    // cctor
    public Item(Item copy)
    {
        sprite = copy.sprite;
        amount = copy.amount;
        name = copy.name;
    }

    protected void Start () 
	{
        if (saveDataKey == "null")
            saveDataKey = Player.gameInstance + "-" + name + "-";
        else if (PlayerPrefs.GetInt(saveDataKey + obtainedKey) == 1)
            Destroy(gameObject);
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
        Player.Instance.Inventory.AddToInventory(this, amount);
        PlayerPrefs.SetInt(saveDataKey + obtainedKey, 1);
        Destroy(gameObject);
    }
}
