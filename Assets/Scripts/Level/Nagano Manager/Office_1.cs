using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office_1 : MonoBehaviour 
{
    [Header("Resources")]
    [SerializeField]
    private Actor2D cleaner;

    public void Start()
    {
        Player.Instance.allowControls = false;
        Player.Instance.transform.parent.localScale = new Vector3(1.25F, 1.25F, 1.25F);
        StartCoroutine(WaitAndStartDialogue());

    }

    IEnumerator WaitAndStartDialogue()
    {
        Vector2 receptionistFront = new Vector2(-4, -1.33F);
        Player.Instance.MoveTo(receptionistFront);

        while ((Player.Instance.transform.position - (Vector3)receptionistFront).magnitude > 0.05F)
        {
            yield return null;
        }

        DialogueManager.Instance.StartNewDialogue(33);

        yield return null;

        Player.Instance.allowControls = true;
    }

    public void CleanerSequence()
    {

        StartCoroutine(CleanerSequenceCoroutine());
        
    }

    IEnumerator CleanerSequenceCoroutine()
    {
        Player.Instance.allowControls = false;
        cleaner.gameObject.SetActive(true);

        Vector2 waypoint1 = new Vector2(11.5F, -1.78F);
        cleaner.MoveTo(waypoint1);

        while ((cleaner.transform.position - (Vector3)waypoint1).magnitude > 0.1F)
        {
            yield return null;
        }

        Vector2 waypoint2 = new Vector2(15.7F, -1.78F);
        cleaner.MoveTo(waypoint2);

        while ((cleaner.transform.position - (Vector3)waypoint2).magnitude > 0.1F)
        {
            yield return null;
        }

        DialogueManager.Instance.StartNewDialogue(222);

        Vector2 waypoint3 = new Vector2(0, -1.78F);
        cleaner.MoveTo(waypoint3);

        while ((cleaner.transform.position - (Vector3)waypoint3).magnitude > 0.1F)
        {
            yield return null;
        }

        Player.Instance.allowControls = true;
        cleaner.gameObject.SetActive(false);
    }

}
