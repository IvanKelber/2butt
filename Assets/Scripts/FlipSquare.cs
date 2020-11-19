using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSquare : MonoBehaviour
{
    public float torqueSpeed = 1000;
    public float angularVelocityCap = 100;
    public float flipDuration;
    public AudioManager audioManager;
    
    AudioSource audioSource;
    Rigidbody2D body;

    bool flipping = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        if(body.velocity.y != 0) {
            body.drag = 0;
        } else {
            body.drag = 5;
        }
    }

    public void GetKicked(int direction) {
        StartCoroutine(Flip(-direction, flipDuration));
    }

    IEnumerator Flip(int direction, float duration) {
        if(!flipping) {
            audioManager.Play("Kick", audioSource);
            flipping = true;
            float start = Time.time;
            float end = start + duration;
            while(Time.time < end) {
                if(Mathf.Abs(body.angularVelocity) <= angularVelocityCap) {
                    float torque = direction * torqueSpeed * Time.fixedDeltaTime;
                    body.AddTorque(torque);
                }
                yield return new WaitForFixedUpdate();
            }
        }
        flipping = false;
        yield return null;
    }



}
