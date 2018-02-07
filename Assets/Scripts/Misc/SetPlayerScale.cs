using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerScale : MonoBehaviour
{

    public Vector3 scale = new Vector3(1.25F, 1.25F, 1.25F);

    // Use this for initialization
    void Start () {
        Player.Instance.transform.parent.localScale = scale;

    }

}
