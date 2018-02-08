using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom_0 : MonoBehaviour 
{

	protected void Start () 
	{
        StartCoroutine(StartSequence());

        Destroy(Player.Instance.gameObject);
	}

    IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(2);

        DialogueManager.Instance.StartNewDialogue(132);

    }

    public void LoadIntoNextScene()
    {
        LevelTransition.LoadScene("Bedroom", 1);
    }
}
