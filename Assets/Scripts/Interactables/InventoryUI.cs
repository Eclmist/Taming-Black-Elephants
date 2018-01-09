using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour 
{

    [SerializeField] private Image[] slots;
    
    private Inventory inventoryReference;
    private Animator anim;

    private bool inventoryOpen = false;

	protected void Start () 
	{
        anim = GetComponent<Animator>();
        inventoryReference = Player.Instance.Inventory;

        UpdateInventoryUI();
	}

    public void UpdateInventoryUI()
    {
        anim.SetBool("Open", inventoryOpen);

        if (!inventoryReference)
        {
            inventoryReference = Player.Instance.Inventory;

            if (!inventoryReference)
                return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Inventory.inventoryMaxSize)
            {
                ItemData itemAtIndex = inventoryReference.GetItem(i);

                if (itemAtIndex != null)
                {
                    slots[i].sprite = itemAtIndex.sprite;
                    slots[i].color = Color.white;
                }
                else
                {
                    slots[i].color = Color.clear;
                    slots[i].sprite = null;
                }
            }
        }
    }

    public void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;

        UpdateInventoryUI();
    }

    public void OpenInventory()
    {
        inventoryOpen = true;

        UpdateInventoryUI();
    }

    public void CloseInventory()
    {
        inventoryOpen = false;
        UpdateInventoryUI();
    }
}
