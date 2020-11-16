using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkDoor : MonoBehaviour
{

    [Range(0, 5)]
    public float shrinkDuration = 1;

    public void Shrink() {
        StartCoroutine(DoShrink());
    }

    IEnumerator DoShrink() {
        float start = Time.time;
        float end = start + shrinkDuration;
        Vector3 originalScale = transform.localScale;
        Vector3 goalScale = new Vector3(originalScale.x, 0, originalScale.z);
        while(Time.time < end) {
            // Shrink only on y axis;
            float percentComplete = (end - Time.time)/(shrinkDuration);
            percentComplete = Mathf.Clamp01(percentComplete);
            transform.localScale = Vector3.Lerp(goalScale, originalScale, percentComplete);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
