using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Office_4 : MonoBehaviour {

    public static bool dialogue40startedBefore = false;

	// Use this for initialization
	void Start () {
        if (!dialogue40startedBefore)
        {
            StartCoroutine(waitFewSecAndDoStuff());
            dialogue40startedBefore = true;
        }
	}

    IEnumerator waitFewSecAndDoStuff()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.StartNewDialogue(40);
    }
}
