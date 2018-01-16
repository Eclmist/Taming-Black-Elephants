using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float blinkRate = 1;
    [SerializeField] [Range(0,1)] private float randomChangePercent = 0.5F;
    [SerializeField] private bool random;

    private Light light;
    private float intensity;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        intensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (random)
        {
            if (Random.Range(0f, 1f) > blinkRate)
            {
                float changeAmount = randomChangePercent * intensity;

                light.intensity = intensity + Random.Range(-changeAmount, changeAmount);
            }
        }
        else
        {
            float multiplier = Mathf.Sin(Time.time * blinkRate);

            if (multiplier > 0) multiplier = 1;
            if (multiplier < 0) multiplier = 0;

            light.intensity = multiplier * intensity;
        }
	}
}
