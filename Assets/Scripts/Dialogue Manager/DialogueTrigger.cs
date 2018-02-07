using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour, IInteractable 
{
    [SerializeField] private int dialogID;
    [SerializeField] private UnityEvent postDialogueEvent;

    public virtual void Interact()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("Dialog Manager is null!");
            return;
        }

        DialogueManager.Instance.StartNewDialogue(dialogID);
        postDialogueEvent.Invoke();
    }

    public void ChangeDialogueID(int id)
    {
        dialogID = id;
    }

    public void DisableTrigger()
    {
        this.enabled = false;
    }
}
