using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTag : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Collider2D colliderPos;

    private float range;

	// Use this for initialization
	void Start () {
        range = Player.Instance.playerReach;
        colliderPos = GetComponentInParent<Collider2D>();
    }

    private void Update()
    {
        Vector3 centerOfCollider = colliderPos.transform.position + Vector3.Scale(colliderPos.offset, colliderPos.transform.localScale);

        anim.SetBool("Open", ((centerOfCollider - Player.Instance.transform.position).magnitude <= range));
    }
}
