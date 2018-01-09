using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public Sprite sprite;
    public string name;

    public ItemData(Sprite s, string n)
    {
        sprite = s;
        name = n;
    }
}

public class Inventory : MonoBehaviour 
{
    public const int inventoryMaxSize = 5;

    [SerializeField] protected List<ItemData> itemContainer = new List<ItemData>(inventoryMaxSize);

    private InventoryUI inventoryUI;

    // Returns true if success, false if got problem
    public bool AddToInventory(Item item, int amount = 1)
    {
        ItemData data = new ItemData(item.Sprite, item.name);

        if (itemContainer.Count + amount >= inventoryMaxSize)
        {
            return false;
        }

        for ( int i = 0; i < amount; i++)
            itemContainer.Add(data);

        return true;
    }

    public bool RemoveItem(int index)
    {
        if (index >= itemContainer.Count)
            return false;

        itemContainer.RemoveAt(index);

        return true;
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

        return true;
    }

    public void ClearInventory()
    {
        itemContainer.Clear();
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
}
