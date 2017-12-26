using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Node(true, "Dialogue/Base Dialogue Node", typeof(DialogueNodeCanvas))]
public abstract class BaseDialogueNode : Node
{
    public override bool AllowRecursion { get { return true; } }
    public override string GetID { get { return id; } }
    protected const string id = "BaseDialogueNode";

    public override Vector2 MinSize { get { return new Vector2(350, 200); } }
    public override bool AutoLayout { get { return true; } }

    public string characterName;
    public Sprite characterPotrait;
    public bool potraitOnLeft;
    public string dialogText;
    public AudioClip dialogAudio;

    protected const float nodePositionOffset = 10f;
    protected Vector2 scroll;

    protected override void OnCreate()
    {
        base.OnCreate();
        characterName = "Character name";
        dialogText = "Insert dialogue text here";
        characterPotrait = null;
        potraitOnLeft = true;
    }

#if UNITY_EDITOR
    public override void NodeGUI()
    {
        DrawNodeGUIPrefix();

        DrawNodeGUISuffix();
    }

    protected virtual void DrawNodeGUIPrefix()
    {
        EditorGUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal();
        characterPotrait = (Sprite)EditorGUILayout.ObjectField(characterPotrait, typeof(Sprite), false, GUILayout.Width(65f), GUILayout.Height(65f));
        GUILayout.BeginVertical();
        characterName = EditorGUILayout.TextField("", characterName);
        GUILayout.Space(5);
        EditorGUIUtility.labelWidth = 250;
        potraitOnLeft = EditorGUILayout.Toggle("Potrait On Left", potraitOnLeft);
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    protected virtual void DrawNodeGUISuffix()
    {
        EditorStyles.textField.wordWrap = true;

        GUILayout.BeginHorizontal();
        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Height(100));
        dialogText = EditorGUILayout.TextArea(dialogText, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 130;
        dialogAudio = EditorGUILayout.ObjectField("Dialogue Audio:", dialogAudio, typeof(AudioClip), false) as AudioClip;
        if (GUILayout.Button("►", GUILayout.Width(20)))
        {
            if (dialogAudio)
                AudioUtils.PlayClip(dialogAudio);
        }
        GUILayout.EndHorizontal();
    }

#endif

    public abstract BaseDialogueNode GetNextNode();
}

public class DialogueConnectionType : ConnectionKnobStyle
{
    public override string Identifier { get { return "DialogueConnector"; } }
    public override Color Color { get { return Color.cyan; } }
    
}