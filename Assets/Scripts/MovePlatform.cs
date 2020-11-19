using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    Vector3 startPosition;
    float startDistance;
    Coroutine movementCoroutine;
    bool moving = false;
    AudioSource audioSource;

    public Vector3 endPosition;
    public float totalMovementDuration;
    public AudioManager audioManager;


    void Start() {
        startPosition = transform.position;
        startDistance = Vector3.Distance(endPosition, startPosition);
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void MoveToEnd() {
        float duration = totalMovementDuration * Vector3.Distance(transform.position, endPosition)/startDistance;
        if(moving)
            StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine(Move(duration, endPosition));

    }

    public void MoveToStart() {
        float duration = totalMovementDuration * Vector3.Distance(transform.position, startPosition)/startDistance;
        if(moving)
            StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine(Move(duration, startPosition));
    }

    void FixedUpdate() {

    }

    IEnumerator Move(float duration,  Vector3 finalPosition) {
        moving = true;
        float start = Time.time;
        float end = start + duration;
        Vector3 initialPosition = transform.position;

        bool rising = finalPosition == endPosition;
        float audioPercentage = 1 - Vector3.Distance(initialPosition, finalPosition)/startDistance;
        Debug.Log("Rising? " +rising + " percent: " + audioPercentage);
        audioManager.PlayFromPercentage(rising? "Rise" : "Fall", audioSource, audioPercentage);
        // audioManager.Play("Kick", audioSource);
        WaitForFixedUpdate _wait = new WaitForFixedUpdate();
        while(Time.time < end) {
            float percentComplete = (end - Time.time)/duration;
            transform.position = Vector3.Lerp(finalPosition, initialPosition, percentComplete);
            yield return null;
        }
        transform.position = finalPosition;
        moving = false;
    }

}
