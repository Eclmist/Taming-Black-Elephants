/* Allows any scene to be instantly playable,
 * such that not having persistent objects from starting scene would not break debugging processes */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFactory {

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        SetupPlayer();

        SetupBGM();
    }

    static void SetupPlayer()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            return;
        }
        else
        {
            player = Object.Instantiate(Resources.Load("Prefab/Actor/Player/Player") as GameObject);

            if (LevelSetting.Instance != null)
                player.transform.position = LevelSetting.Instance.debugSpawnPoint;

            Player.Instance.UndoMoveTo();
        }
    }

    static void SetupBGM()
    {
        GameObject musicPlayer = GameObject.Find("Jukebox");

        if (musicPlayer != null)
        {
            return;
        }
        else
        {
            musicPlayer = Object.Instantiate(Resources.Load("Prefab/Audio/Jukebox") as GameObject);

            if (LevelSetting.Instance != null)
                musicPlayer.GetComponent<AudioManager>().PlayBGM(LevelSetting.Instance.levelMood);
        }

    }
}
