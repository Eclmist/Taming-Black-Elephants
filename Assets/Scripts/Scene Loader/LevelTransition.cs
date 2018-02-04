using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelTransition : MonoBehaviour, IInteractable {

    public string targetScene = "";
    public int targetIndex;

    public bool doConfirmationPrompt;
    public bool ignoreCollisionTrigger;
    public bool enabled = true;

    public void Interact()
    {
        if (!enabled)
            return;

        if (SpawnManager.currentScene == targetScene)
            return;

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
            LoadScene(targetScene, targetIndex);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (ignoreCollisionTrigger)
            return;

        if (SpawnManager.currentScene == targetScene)
            return;

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
        LoadScene(targetScene, targetIndex);
    }

    public static void LoadScene(string targetScene, int targetIndex)
    {

        // Handle persistent BGM
        LevelSetting.lastKnownMood = LevelSetting.Instance.levelMood;

        Initiate.Fade(targetScene, Color.black, 2.0f);
        Player.Instance.allowInteractions = false;

        if (SpawnManager.Instance == null)
        {
            GameObject newSpawnManager = new GameObject();
            newSpawnManager.AddComponent<SpawnManager>();
        }

        SpawnManager.Instance.TargetPortalIndex = targetIndex;
    }
}
