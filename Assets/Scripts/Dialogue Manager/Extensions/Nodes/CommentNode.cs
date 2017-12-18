using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using UnityEditor;

[Node(false, "Comment", typeof(DialogueNodeCanvas))]
public class CommentNode : Node 
{
    public override string GetID { get { return id; } }
    protected const string id = "Comment";

    public override Vector2 MinSize { get { return new Vector2(350, 0); } }
    public override bool AutoLayout { get { return true; } }

    public string comment;
    protected Vector2 scroll;

    public override void NodeGUI()
    {
        EditorStyles.textField.wordWrap = true;

        GUILayout.BeginHorizontal();
        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Height(100));
        comment = EditorGUILayout.TextArea(comment, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();
        GUILayout.EndHorizontal();

    }
}
