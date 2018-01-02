﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelTransition : MonoBehaviour, IInteractable {

    public string targetScene = "";
    public int targetIndex;

    public bool doConfirmationPrompt;

    public void Interact()
    {
        if (doConfirmationPrompt)
        {
            DialogueUIManager.Instance.ShowGenericOptions
            (
                new GenericAction("Go to " + Regex.Replace(targetScene, "(\\B[A-Z])", " $1"), LoadScene),
                new GenericAction("Back", null)
            );
        }
        else
        {
            LoadScene();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Interact();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        SpawnManager.currentScene = SceneManager.GetActiveScene().name;
    }

    private void LoadScene()
    {
        if (SpawnManager.currentScene == targetScene)
            return;

        Initiate.Fade(targetScene, Color.black, 2.0f);

        if (SpawnManager.Instance == null)
        {
            GameObject newSpawnManager = new GameObject();
            newSpawnManager.AddComponent<SpawnManager>();
        }

        SpawnManager.Instance.TargetPortalIndex = targetIndex;
    }
}
