using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    Vector3 startPosition;
    float startDistance;
    Coroutine movementCoroutine;
    bool moving = false;

    public Vector3 endPosition;
    public float totalMovementDuration;

    void Start() {
        startPosition = transform.position;
        startDistance = Vector3.Distance(endPosition, startPosition);
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

    IEnumerator Move(float duration,  Vector3 finalPosition) {
        moving = true;
        float start = Time.time;
        float end = start + duration;
        Vector3 initialPosition = transform.position;
        while(Time.time < end) {
            float percentComplete = (end - Time.time)/duration;
            transform.position = Vector3.Lerp(finalPosition, initialPosition, percentComplete);
            yield return null;
        }
        transform.position = finalPosition;
        moving = false;
    }

}
