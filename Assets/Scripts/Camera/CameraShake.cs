﻿///Daniel Moore (Firedan1176) - Firedan1176.webs.com/
///26 Dec 2015
///
///Shakes camera parent object

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    public bool debugMode = false;//Test-run/Call ShakeCamera() on start

    public float shakeAmount;//The amount to shake this frame.
    public float shakeDuration;//The duration this frame.

    //Readonly values...
    float shakePercentage;//A percentage (0-1) representing the amount of shake to be applied when setting rotation.
    float startAmount;//The initial shake amount (to determine percentage), set when ShakeCamera is called.
    float startDuration;//The initial shake duration, set when ShakeCamera is called.

    bool isRunning = false; //Is the coroutine running right now?

    public bool smooth;//Smooth rotation?
    public float smoothAmount = 5f;//Amount to smooth

    void Start()
    {

        if (debugMode) ShakeCamera();
    }


    public void ShakeCamera()
    {
        startAmount = 1;
        startDuration = 0.5F;

        shakeAmount = startAmount;
        shakeDuration = startDuration;

        if (!isRunning) StartCoroutine(Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
    }

    public void ShakeCameraHarder()
    {
        startAmount = 1;
        startDuration = 1.5F;

        shakeAmount = startAmount;
        shakeDuration = startDuration;

        if (!isRunning) StartCoroutine(Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
    }


    public void ShakeCamera(float amount, float duration)
    {

        shakeAmount += amount;//Add to the current amount.
        startAmount = shakeAmount;//Reset the start amount, to determine percentage.
        shakeDuration += duration;//Add to the current time.
        startDuration = shakeDuration;//Reset the start time.

        if (!isRunning) StartCoroutine(Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
    }


    IEnumerator Shake()
    {
        isRunning = true;

        while (shakeDuration > 0.01f)
        {
            Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;//A Vector3 to add to the Local Rotation
            rotationAmount.z = 0;//Don't change the Z; it looks funny.

            shakePercentage = shakeDuration / startDuration;//Used to set the amount of shake (% * startAmount).

            shakeAmount = startAmount * shakePercentage;//Set the amount of shake (% * startAmount).
            shakeDuration -= Time.unscaledDeltaTime;


            if (smooth)
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.unscaledDeltaTime * smoothAmount);
            else
                transform.localRotation = Quaternion.Euler(rotationAmount);//Set the local rotation the be the rotation amount.

            yield return null;
        }
        transform.localRotation = Quaternion.identity;//Set the local rotation to 0 when done, just to get rid of any fudging stuff.
        isRunning = false;
    }

}