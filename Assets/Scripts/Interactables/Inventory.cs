using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public Sprite sprite;
    public Sprite inspectionSprite;
    public string name;

    public ItemData(Sprite s, Sprite iSprite, string n)
    {
        sprite = s;
        inspectionSprite = iSprite;
        name = n;
    }
}

public class Inventory : MonoBehaviour
{
    public const int inventoryMaxSize = 5;

    [SerializeField] protected List<ItemData> itemContainer = new List<ItemData>(inventoryMaxSize);

    private InventoryUI inventoryUI;
    private Animator itemPickupAnim;

    // Returns true if success, false if got problem
    public bool AddToInventory(Item item, int amount = 1, bool showEffect = true)
    {
        ItemData data = new ItemData(item.Sprite, item.InspectionSprite, item.name);

        if (itemContainer.Count + amount >= inventoryMaxSize)
        {
            return false;
        }

        for (int i = 0; i < amount; i++)
            itemContainer.Add(data);


        if (showEffect)
        {
            if (!inventoryUI)
                inventoryUI = FindObjectOfType<InventoryUI>();

            inventoryUI.itemObtainedAnim.SetTrigger("Enable");
            inventoryUI.itemName_Obtained.text = item.name + " obtained!";

        }

        SendUIUpdateEvent();

        return true;
    }

    public bool RemoveItem(int index)
    {
        if (index >= itemContainer.Count)
            return false;

        itemContainer.RemoveAt(index);
        SendUIUpdateEvent();
        return true;
    }

    public bool HasItem(string name)
    {
        foreach (ItemData item in itemContainer)
        {
            if (item.name == name)
                return true;
        }

        return false;
    }

    public bool RemoveItem(string name, int amount = 1)
    {
        if (amount <= 0)
            return false;

        int removed = 0;

        for (int i = 0; i < itemContainer.Count; i++)
        {
            if (itemContainer[i].name == name)
            {
                RemoveItem(i);
                i--; //Since next item's index has decreased
                removed++;

                if (removed >= amount)
                    break;
            }
        }
        SendUIUpdateEvent();
        return true;
    }

    public void ClearInventory()
    {
        itemContainer.Clear();
        SendUIUpdateEvent();

    }

    public ItemData GetItem(int index)
    {
        if (index >= itemContainer.Count)
        {
            return null;
        }
        else
        {
            return itemContainer[index];
        }
    }

    private void SendUIUpdateEvent()
    {
        if (!inventoryUI)
            inventoryUI = FindObjectOfType<InventoryUI>();

        inventoryUI.BroadcastMessage("UpdateInventoryUI");
    }

    public int UniqueItemCount()
    {
        return itemContainer.Count;
    }
}
