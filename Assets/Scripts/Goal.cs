using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;


public class Goal : MonoBehaviour
{

    public GameEvent onGoalReached;
    public LayerMask playerMask;

    void OnTriggerEnter2D(Collider2D collision) {
        if((playerMask.value & 1 << collision.gameObject.layer) != 0) {
            onGoalReached.Raise();
        }
    }

}
