using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour 
{
    public static SpawnManager Instance;
    public static string currentScene = "";

    private int targetPortalIndex = -1;
    
    public int TargetPortalIndex
    {
        get { return targetPortalIndex; }
        set { targetPortalIndex = value; }
    }

    protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        currentScene = SceneManager.GetActiveScene().name;

        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += OnSceneChanged;
       
    }

    private void OnSceneChanged(Scene s0, Scene s1)
    {
        if (targetPortalIndex == -1)
            return;

        Vector2 targetSpawnPos = Vector2.zero;

        bool foundSpawnPoint = false;
        GameObject existingPlayerObj = null;

        // Check if portal collection available
        foreach (GameObject obj in s1.GetRootGameObjects())
        {
            LevelPortalCollection spawnCollection = obj.GetComponent<LevelPortalCollection>();

            if (spawnCollection != null)
            {
                targetSpawnPos = spawnCollection.GetPortalLocation(TargetPortalIndex);
                foundSpawnPoint = true;
            }
            else if (obj.tag == "Player")
            {
                existingPlayerObj = obj;
            }
        }

        if (!foundSpawnPoint)
        {
            Debug.Log("gg no spawn point");
            return;
        }

        if (existingPlayerObj != null)
        {
            existingPlayerObj.transform.position = targetSpawnPos;
        }
        else
        {
            Instantiate<GameObject>(Resources.Load("Player") as GameObject,
                targetSpawnPos, Quaternion.identity);
        }
    }
}
