using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public Renderer spriteRen;
    public Light light;

    public float uvDiff = 0.0003F;
    public float toggleSpeed = 1;
    private float lightIntensityOriginal;

    public bool on = true;

    private float randomOffset;

    public void Start()
    {
        lightIntensityOriginal = light.intensity;
        randomOffset = Random.Range(0, toggleSpeed * 5);
    }

    // Update is called once per frame
    void Update()
    {

        if (on)
        {
            if (spriteRen != null)
            {
                spriteRen.material.SetTextureOffset("_MainTex", new Vector2(0, Random.Range(-uvDiff, uvDiff)));

                Color newColor = Color.white;
                newColor.a = Random.Range(0.3F, 1);

                spriteRen.material.SetColor("_TintColor", newColor);

            }

            if (light != null)
            {
                light.intensity = lightIntensityOriginal + Random.Range(-10f, 10f);
            }
        }


        Toggle((Mathf.Sin((randomOffset + Time.time) * toggleSpeed) > 0));
    }


    void Toggle(bool active)
    {
        on = active;
        if (active == false)
        {
            if (spriteRen != null)
            {

                Color newColor = Color.black;
                spriteRen.material.SetColor("_TintColor", newColor);
            }

            if (light != null)
            {
                light.intensity = 0;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!on)
            return;

        if (other.tag == "Player")
        {
            Player.Instance.Die();
        }
    }
}
