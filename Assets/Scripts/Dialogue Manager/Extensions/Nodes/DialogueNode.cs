﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;


#if UNITY_EDITOR
using UnityEditor;
#endif

[Node (false, "Dialogue/Basic Node", typeof(DialogueNodeCanvas))]
public class DialogueNode : BaseDialogueNode 
{
    public override string GetID { get { return id; } }
    protected new const string id = "DefaultDialogueNode";

    public override string Title { get { return "Basic Dialog"; } }

    [ConnectionKnob("Input", Direction.In, ConnectionCount.Multi, NodeSide.Left, nodePositionOffset)]
    public ConnectionKnob inputKnob;

    [ConnectionKnob("Next", Direction.Out, "DialogueConnector", NodeSide.Right, nodePositionOffset)]
    public ConnectionKnob outputKnob;

    public bool isThinking = false;

    public override void NodeGUI()
    {
        DrawNodeGUIPrefix();

        isThinking = EditorGUILayout.Toggle("ThoughtBubble?", isThinking);

        DrawNodeGUISuffix();
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
