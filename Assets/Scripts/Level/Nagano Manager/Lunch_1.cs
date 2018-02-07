using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lunch_1 : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        DialogueManager.Instance.StartNewDialogue(0);
	}

    public void GoBackToOffice()
    {
        LevelTransition.LoadScene("BunnyOffice_After_Lunch", 0);
    }
}
