using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEventManager : MonoBehaviour 
{
    public static DialogueEventManager Instance;

    [SerializeField]
    public List<EventHolder> eventList = new List<EventHolder>();

    [System.Serializable]
    public struct EventHolder
    {
        public int ID;
        public UnityEvent unityEvent;
    }

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

}
