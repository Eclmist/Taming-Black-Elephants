using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;


[Node(false, "Dialogue/Multiple Options Node", typeof(DialogueNodeCanvas))]
public class DialogueMultiOptionNode : BaseDialogueNode
{
    // Mandatory ID
    public override string GetID { get { return id; } }
    private new const string id = "MultipleOptionsDialogueNode";

    // Set Node title
    public override string Title { get { return "Multiple Options Selector"; } }

    // Set size
    public override Vector2 MinSize { get { return new Vector2(320, 0); } }

    //previous node connections
    [ConnectionKnob("Input", Direction.In, "DialogueConnector", NodeSide.Left, nodePositionOffset)]
    public ConnectionKnob inputKnob;

    private const int StartValue = 276;
    private const int SizeValue = 24;

    [SerializeField]
    List<DataHolderForOption> _options;

    private ConnectionKnobAttribute dynaCreationAttribute
        = new ConnectionKnobAttribute(
           "Next", Direction.Out, "DialogueConnector", NodeSide.Right);

    private int selectedOptionIndex = -1;

    protected override void OnCreate()
    {
        base.OnCreate();
        _options = new List<DataHolderForOption>();
        Debug.Log("Created");
        AddNewOption();
    }

#if UNITY_EDITOR

    public override void NodeGUI()
    {
        //base.NodeGUI();

        //GUILayout.Space(5);

        #region Options
        GUILayout.BeginVertical("box");
        GUILayout.ExpandWidth(false);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Options", NodeEditorGUI.nodeLabelBoldCentered);
        if (GUILayout.Button("+", GUILayout.Width(20)))
        {
            AddNewOption();
            IssueEditorCallBacks();
        }

        GUILayout.EndHorizontal();
        GUILayout.Space(5);

        DrawOptions();

        GUILayout.ExpandWidth(false);
        GUILayout.EndVertical();
        #endregion

    }

    private void DrawOptions()
    {
        EditorGUILayout.BeginVertical();
        for (var i = 0; i < _options.Count; i++)
        {
            DataHolderForOption option = _options[i];
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(i + ".", GUILayout.MaxWidth(15));
            option.OptionDisplay = EditorGUILayout.TextArea(option.OptionDisplay, GUILayout.MinWidth(80));
            ((ConnectionKnob)dynamicConnectionPorts[i]).SetPosition();
            if (GUILayout.Button("‒", GUILayout.Width(20)))
            {
                _options.RemoveAt(i);
                DeleteConnectionPort(i);
                i--;
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.Space(4);
        }
        GUILayout.EndVertical();
    }

#endif

    private void AddNewOption()
    {
        DataHolderForOption option = new DataHolderForOption { OptionDisplay = "Write Here" };
        CreateConnectionKnob(dynaCreationAttribute);
        _options.Add(option);
    }

    //For Resolving the Type Mismatch Issue
    private void IssueEditorCallBacks()
    {
        NodeEditorCallbacks.IssueOnAddConnectionPort(dynamicConnectionPorts[_options.Count - 1]);
    }

    [Serializable]
    class DataHolderForOption
    {
        public string OptionDisplay;
        //public int dynamicConnectionPortsIndex;				
    }

    public List<string> GetAllOptions()
    {
        return _options.Select(option => option.OptionDisplay).ToList();
    }

    public override BaseDialogueNode GetNextNode()
    {
        if (selectedOptionIndex != -1 && selectedOptionIndex < dynamicConnectionPorts.Count)
        {
            if (dynamicConnectionPorts[selectedOptionIndex].connections.Count > 0)
                return dynamicConnectionPorts[selectedOptionIndex].connections[0].body as BaseDialogueNode;

            return null;
        }

        return null;
    }

    public virtual void SetSelectedOptionIndex(int index)
    {
        Debug.Assert(index < _options.Count && index >= 0);

        selectedOptionIndex = index;
    }
}