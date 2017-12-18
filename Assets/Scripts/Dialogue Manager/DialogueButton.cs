using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DialogueButton : MonoBehaviour 
{
    public Button button;

    public void Start()
    {
        button = GetComponent<Button>();
    }
}
