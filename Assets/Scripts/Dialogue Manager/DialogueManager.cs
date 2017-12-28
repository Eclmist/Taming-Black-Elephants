using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Settings")]
    [SerializeField] [Range(0, 1)] private float textSpeed = 1;
    [SerializeField] private bool autoAdvance;
    [SerializeField] private bool freezeTime = true;

    [Header("Resources")]
    [SerializeField] private DialogueNodeCanvas dialogueCanvas;

    private DialogueUIManager uiManager;

    protected void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;

        uiManager = DialogueUIManager.Instance;

        if (dialogueCanvas != null)
        {
            if (uiManager != null)
            {
                uiManager.SetFreezeTime(freezeTime);

                return;
            }
        }

        Debug.LogError("Dialog Canvas or UI Manager cannot be found! Dialogs will not work.");
        Debug.LogError("dialogueCanvas = " + dialogueCanvas);
        Debug.LogError("uiManager = " + uiManager);
        Destroy(this);
    }

    public DialogueStartNode GetDialogueByID(int id)
    {
        foreach (Node n in dialogueCanvas.nodes)
        {
            DialogueStartNode startNode;
            if (n is DialogueStartNode)
            {
                startNode = n as DialogueStartNode;

                if (startNode.dialogueID == id)
                    return startNode;
            }
        }

        return null;
    }

    public void StartNewDialogue(int id)
    {
        DialogueStartNode startNode = GetDialogueByID(id);

        if (!startNode)
            return;

        StopAllCoroutines();
        StartCoroutine(DialogueCoroutine(startNode));
    }

    protected IEnumerator DialogueCoroutine(BaseDialogueNode node)
    {
        uiManager.ToggleDialogueUI(true);

        if (node.GetType() == typeof(DialogueCallbackNode))
        {
            uiManager.ToggleDialogueUI(false);

            DialogueCallbackNode eventNode = node as DialogueCallbackNode;
            int indexToCall = eventNode.callbackEventID;

            foreach (DialogueEventManager.EventHolder holder in DialogueEventManager.Instance.eventList)
            {
                if (indexToCall == holder.ID)
                {
                    holder.unityEvent.Invoke();
                }
            }
        }
        else if (node is DialogueMultiOptionNode)
        {
            DialogueMultiOptionNode optionsNode = node as DialogueMultiOptionNode;

            uiManager.ShowOptions(optionsNode.GetAllOptions().ToArray());


            // Wait until option selected
            while (uiManager.GetSelectedOptionIndex() == -1)
            {
                yield return null;
            }
            int selectedOptionIndex = uiManager.GetSelectedOptionIndex();

            // set int in multioptionnode to determine what getnextnode returns
            optionsNode.SetSelectedOptionIndex(selectedOptionIndex);
        }
        else if (node is DialogueNode)
        {
            uiManager.DisplayNewMessage(node.characterName, node.dialogText, textSpeed,
                node.characterPotrait, node.potraitOnLeft);

            // Get rid of 1 mouse click
            yield return null;


            while (uiManager.IsMessageCompletelyShown() == false)
            {
                // Skip typewriter if mouse down
                if (Input.GetMouseButtonDown(0) == true || Input.touchCount > 0)
                    uiManager.SkipTypewriter();

                yield return null;
            }


            // Check for auto advance
            if (!autoAdvance)
            {
                // wait for input to proceed to next node
                while (Input.GetMouseButtonDown(0) == false && Input.touchCount == 0)
                {
                    yield return null;
                }

                yield return null;
            }
        }
        
        if (node.GetNextNode() != null)
        {
            yield return StartCoroutine(DialogueCoroutine(node.GetNextNode()));
        }

        uiManager.ToggleDialogueUI(false);
    }
}
