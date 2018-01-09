using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Sprite_Pulse : MonoBehaviour 
{
    [SerializeField] private float pulseSpeed = 2;

    private SpriteRenderer ren;
    private Color glowColor;

	protected void Start () 
	{
        ren = GetComponent<SpriteRenderer>();

        if (ren.material.GetFloat("_Outline") == 0)
        {
            Debug.Log("Sprite Pulse can only work with shaders of type sprite-diffuse-outline!");
            Destroy(this);
        }

        glowColor = ren.material.GetColor("_OutlineColor");
	}
	
	protected void Update () 
	{
        Color newGlowAlpha = glowColor;
        newGlowAlpha.a = (Mathf.Sin(Time.time * pulseSpeed) + 1) / 2;

        ren.material.SetColor("_OutlineColor", newGlowAlpha);
	}
}
