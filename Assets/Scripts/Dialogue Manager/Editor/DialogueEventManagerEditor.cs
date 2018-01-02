using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Linq;
using UnityEngine.Events;

[CustomEditor(typeof(DialogueEventManager))]
public class DialogueEventManagerEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        SerializedProperty prop = serializedObject.FindProperty("eventList");

        list = new ReorderableList(serializedObject, prop, true, true, true, true);

        list.elementHeightCallback = (index) => {

            SerializedProperty ue = 
                list.serializedProperty.GetArrayElementAtIndex(index).
                FindPropertyRelative("unityEvent");

            SerializedProperty persistentCalls = ue.FindPropertyRelative("m_PersistentCalls.m_Calls");


            return 86 + (Mathf.Max(0, (persistentCalls.arraySize-1)) * 43);
        };

        list.drawElementCallback = (rect, index, active, focused) => 
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("ID"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("unityEvent"), GUIContent.none);
        };

        list.drawHeaderCallback = (Rect rect) => {

            float x = 100;
            EditorGUI.LabelField(
                new Rect(rect.x, rect.y, x, rect.height),
                "ID");

            EditorGUI.LabelField(
                new Rect(x, rect.y, rect.width - x, rect.height),
                "Event");
        };
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();





    }

}
