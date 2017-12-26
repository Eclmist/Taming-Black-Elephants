using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using UnityEditor;

[Node(false, "Dialogue/Start Node", typeof(DialogueNodeCanvas))]
public class DialogueStartNode : BaseDialogueNode 
{
    // Mandatory ID
    public override string GetID { get { return id; } }
    private new const string id = "DialogueStartNode";

    // Set Node title
    public override string Title { get { return "Dialogue Start"; } }

    // Set Node Size
    public override Vector2 MinSize
    {
        get
        {
            return new Vector2(200, 50);
        }
    }

    // Current Dialog identifier
    public int dialogueID;

    // Output knob
    [ConnectionKnob("Next", Direction.Out, "DialogueConnector", NodeSide.Right, nodePositionOffset)]
    public ConnectionKnob outputKnob;

    public override void NodeGUI()
    {
        //DrawNodeGUIPrefix();
        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        EditorGUIUtility.labelWidth = 80;
        dialogueID = EditorGUILayout.IntField("DialogueID", dialogueID, GUILayout.ExpandWidth(true));
        GUILayout.Space(5);
        GUILayout.EndVertical();

        //DrawNodeGUISuffix();
    }

    public override BaseDialogueNode GetNextNode()
    {
        if (outputKnob.connected())
        {
            return outputKnob.connection(0).body as BaseDialogueNode;
        }
        else
        {
            return null;
        }
    }
}
