using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour 
{

    public Animator itemObtainedAnim;
    public Text itemName_Obtained;

    [SerializeField] private Image[] slots;
    
    private Inventory inventoryReference;
    private Animator anim;

    private bool inventoryOpen = false;

    [Header("Item Inspection UI Effect")]
    [SerializeField] private CanvasGroup inspectionUI;
    [SerializeField] private Image inspectionItemSprite;

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

    public void InspectItem(int index)
    {
        if (index >= inventoryReference.UniqueItemCount())
        {
            return;
        }

        inspectionUI.gameObject.SetActive(true);
        inspectionUI.alpha = 1;

        Time.timeScale = 0;

        inspectionItemSprite.sprite = inventoryReference.GetItem(index).sprite;
    }

    public void HideItemInspectionUI()
    {
        inspectionUI.alpha = 0;
        inspectionUI.gameObject.SetActive(false);

        Time.timeScale = 1;

        inspectionItemSprite.sprite = null;
    }
}
