using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelTransition : MonoBehaviour {

    public string targetScene = "";

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        Initiate.Fade(targetScene, Color.black, 2.0f);
    }


}
