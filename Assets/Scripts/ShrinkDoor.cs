﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkDoor : MonoBehaviour
{

    [Range(0, 5)]
    public float totalShrinkDuration = 1;
    public Vector3 endScale;

    Vector3 startScale;
    float startDistance;
    Coroutine scalingCoroutine;
    bool scalingUp, scalingDown;


    void Start() {
        startScale = transform.localScale;
        startDistance = Vector3.Distance(endScale, startScale);
    }

    public void ScaleToEnd() {
        float duration = totalShrinkDuration * Vector3.Distance(transform.localScale, endScale)/startDistance;
        if(scalingDown)
            return;
        if(scalingUp)
            StopCoroutine(scalingCoroutine);
        scalingDown = true;
        scalingCoroutine = StartCoroutine(Scale(duration, endScale));
    }

    public void ScaleToStart() {
        float duration = totalShrinkDuration * Vector3.Distance(transform.localScale, startScale)/startDistance;
        if(scalingUp) {
            return;
        }
        if(scalingDown)
            StopCoroutine(scalingCoroutine);
        scalingUp = true;
        scalingCoroutine = StartCoroutine(Scale(duration, startScale));
    }

    IEnumerator Scale(float duration,  Vector3 finalScale) {
        float start = Time.time;
        float end = start + duration;
        Vector3 initialScale = transform.localScale;
        while(Time.time < end) {
            float percentComplete = (end - Time.time)/duration;
            transform.localScale = Vector3.Lerp(finalScale, initialScale, percentComplete);
            yield return null;
        }
        transform.localScale = finalScale;
        scalingUp = false;
        scalingDown = false;
    }

}
