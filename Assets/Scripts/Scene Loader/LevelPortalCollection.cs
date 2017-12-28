using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortalCollection : MonoBehaviour 
{
    public LevelTransition[] portals;

    public Vector2 GetPortalLocation(int index)
    {
        return portals[index].transform.position;
    }
}
 