using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Item, IInteractable 
{
    [SerializeField] private int dialogID;

    public override void Interact()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("Dialog Manager is null!");
            return;
        }

        DialogueManager.Instance.StartNewDialogue(dialogID);
    }
}
