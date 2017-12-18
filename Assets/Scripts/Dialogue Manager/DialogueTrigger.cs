using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour 
{
    [SerializeField] private int dialogID;

    protected void Update () 
	{
        if (Input.GetKeyDown(KeyCode.L)) // TODO: Replace with whatever
        {
            if (DialogueManager.Instance == null)
            {
                Debug.LogError("Dialog Manager is null!");
                return;
            }

            DialogueManager.Instance.StartNewDialogue(dialogID);
        }
	}
}
