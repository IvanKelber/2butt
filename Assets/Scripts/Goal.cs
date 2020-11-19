using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;


public class Goal : MonoBehaviour
{

    public GameEvent onGoalReached;
    public LayerMask playerMask;
    public AudioManager audioManager;
    AudioSource audioSource;

    void Start() {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if((playerMask.value & 1 << collision.gameObject.layer) != 0) {
            audioManager.Play("Goal", audioSource);
            onGoalReached.Raise();
        }
    }

}
