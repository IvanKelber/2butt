using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSquare : ResetableObject
{
    public float torqueSpeed = 1000;
    public float angularVelocityCap = 100;
    public float flipDuration;
    Rigidbody2D body;

    bool flipping = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) {
            StartCoroutine(Flip(-1, flipDuration));
        }
        if(Input.GetKeyDown(KeyCode.J)) {
            StartCoroutine(Flip(1, flipDuration));
        }
    }

    void FixedUpdate() {

    }

    public void GetKicked(int direction) {
        StartCoroutine(Flip(-direction, flipDuration));
    }

    IEnumerator Flip(int direction, float duration) {
        if(!flipping) {
            flipping = true;
            float start = Time.time;
            float end = start + duration;
            Debug.Log(start +", " + end);
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
