using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NodeEditorFramework;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Events;

[Node(false, "Dialogue/Callback Node", typeof(DialogueNodeCanvas))]
public class DialogueCallbackNode : BaseDialogueNode
{
    public override string GetID { get { return id; } }

    protected new const string id = "DialogueCallbackNode";

    public override string Title { get { return "Callback"; } }

    public override bool AutoLayout { get { return true; } }
    public override Vector2 MinSize { get { return new Vector2(200, 50); } }

    [ConnectionKnob("Input", Direction.In, "DialogueConnector", NodeSide.Left, 10f)]
    public ConnectionKnob inputKnob;

    [ConnectionKnob("Next", Direction.Out, "DialogueConnector", NodeSide.Right, 10f)]
    public ConnectionKnob outputKnob;

    [SerializeField]
    public int callbackEventID;

    protected override void OnCreate()
    {
        base.OnCreate();
        callbackEventID = -1;
    }


#if UNITY_EDITOR
    public override void NodeGUI()
    {

        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        EditorGUIUtility.labelWidth = 80;
        callbackEventID = EditorGUILayout.IntField("Callback ID", callbackEventID, GUILayout.ExpandWidth(true));
        GUILayout.Space(5);
        GUILayout.EndVertical();

    }
#endif
    public override BaseDialogueNode GetNextNode()
    {
        if (outputKnob.connections.Count <= 0)
            return null;

        return outputKnob.connections[0].body as BaseDialogueNode;
    }
}