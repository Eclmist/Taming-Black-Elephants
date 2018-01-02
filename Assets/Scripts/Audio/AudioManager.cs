using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("After scene is loaded and game is running");
    }
}

