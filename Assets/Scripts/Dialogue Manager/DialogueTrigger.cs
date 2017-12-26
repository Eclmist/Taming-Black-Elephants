using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable 
{
    [SerializeField] private int dialogID;

    public void Interact()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("Dialog Manager is null!");
            return;
        }

        DialogueManager.Instance.StartNewDialogue(dialogID);
    }
}
