using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    Vector3 startPosition;
    float startDistance;
    Coroutine movementCoroutine;
    bool Moving {get {
        return movingUp || movingDown;
    }}
    bool movingUp, movingDown;
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
        if(movingUp) {
            return;
        }
        if(movementCoroutine != null)
            StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine(Move(duration, endPosition));

    }

    public void MoveToStart() {
        float duration = totalMovementDuration * Vector3.Distance(transform.position, startPosition)/startDistance;
        if(movingDown) {
            return;
        }
        if(movementCoroutine != null)
            StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine(Move(duration, startPosition));
    
    }

    void FixedUpdate() {

    }

    IEnumerator Move(float duration,  Vector3 finalPosition) {
        movingUp = finalPosition == endPosition;
        movingDown = !movingUp;
        float start = Time.time;
        float end = start + duration;
        Vector3 initialPosition = transform.position;

        float audioPercentage = 1 - Vector3.Distance(initialPosition, finalPosition)/startDistance;
        audioManager.PlayFromPercentage(movingUp? "Rise" : "Fall", audioSource, audioPercentage);
        // audioManager.Play("Kick", audioSource);
        WaitForFixedUpdate _wait = new WaitForFixedUpdate();
        while(Time.time < end) {
            float percentComplete = (end - Time.time)/duration;
            transform.position = Vector3.Lerp(finalPosition, initialPosition, percentComplete);
            yield return null;
        }
        transform.position = finalPosition;
        movingUp = movingDown = false;
    }

}
