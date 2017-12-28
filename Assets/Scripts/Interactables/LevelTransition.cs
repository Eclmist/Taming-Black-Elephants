using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelTransition : MonoBehaviour {

    public string targetScene = "";
    public int targetIndex;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (SpawnManager.currentScene != targetScene)
            LoadScene();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        SpawnManager.currentScene = SceneManager.GetActiveScene().name;
    }

    private void LoadScene()
    {
        Initiate.Fade(targetScene, Color.black, 2.0f);

        if (SpawnManager.Instance == null)
        {
            GameObject newSpawnManager = new GameObject();
            newSpawnManager.AddComponent<SpawnManager>();
        }

        SpawnManager.Instance.TargetPortalIndex = targetIndex;
    }
}
