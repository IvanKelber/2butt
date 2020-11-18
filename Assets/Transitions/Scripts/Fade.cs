using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [Range(0.1f, 5)]
    public float fadeDuration = 1;
    public ScreenTransitionImageEffect imageEffect;
    // Start is called before the first frame update
    void Start()
    {
        if(imageEffect == null)
            imageEffect = GetComponent<ScreenTransitionImageEffect>() as ScreenTransitionImageEffect;
    }

    void Update() {

    }

    public IEnumerator FadeOut() {
        float start = Time.time;
        float end = start + fadeDuration;
        imageEffect.maskInvert = false;
        imageEffect.maskValue = 0;
        imageEffect.Enable();
        // Fade Out
        while(Time.time < end) {
            float percentage = 1 - (end - Time.time)/fadeDuration;
            imageEffect.maskValue = Mathf.Lerp(0,1, percentage);
            yield return null;
        }
        imageEffect.maskValue = 1;
        yield return null;
    }

    public IEnumerator FadeInInverted() {
        imageEffect.maskInvert = true;
        imageEffect.maskValue = 0;
        float start = Time.time;
        float end = start + fadeDuration;
        //Fade in
        while(Time.time < end) {
            float percentage = 1 - (end - Time.time)/fadeDuration;
            imageEffect.maskValue = Mathf.Lerp(0,1, percentage);
            yield return null;
        }
        imageEffect.maskValue = 1;
        imageEffect.Disable();
        yield return null;
    }
}
