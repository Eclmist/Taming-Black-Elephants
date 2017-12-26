using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
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

    }


}
