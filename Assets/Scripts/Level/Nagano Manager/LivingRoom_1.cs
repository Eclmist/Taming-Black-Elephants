using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoom_1 : MonoBehaviour 
{
    [Header("Resources")]
    [SerializeField]
    private DialogueNodeCanvas dialogueCanvasAfterBreakfast;

    public void CheckForCarrotAndStartDialogue()
    {
        if (Player.Instance.Inventory.HasItem("Carrot"))
        {
            DialogueManager.Instance.StartNewDialogue(21);
        }
        else
        {
            DialogueManager.Instance.StartNewDialogue(20);
        }
    }

    public void SwapCanvasToAfterBreakfastCanvas()
    {
        DialogueManager.Instance.DialogueCanvas = dialogueCanvasAfterBreakfast;
    }

    public void RemoveCarrot()
    {
        Player.Instance.Inventory.RemoveItem("Carrot");
    }

    public void GoToOffice()
    {
        LevelTransition.LoadScene("Office", 0);
    }
}
