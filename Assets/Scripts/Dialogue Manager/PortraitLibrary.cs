using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitLibrary : MonoBehaviour 
{

    public static Sprite GetPortrait(string name)
    {
        Sprite s = Resources.Load<Sprite>("Portraits/" + name);
        return s;
    }
}
