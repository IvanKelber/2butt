using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class ButtonPress : MonoBehaviour
{
    BoxCollider2D collider;

    [SerializeField]
    GameEvent onPress, onUnpress;

    AudioManager audioManager;
    AudioSource audioSource;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        audioManager = transform.parent.GetComponent<FlipSquare>().audioManager;
        audioSource = gameObject.AddComponent<AudioSource>();
    }


    void OnTriggerEnter2D() {
        onPress.Raise();
        audioManager.Play("ButtonClick", audioSource);
    }

    void OnTriggerStay2D() {
        onPress.Raise();
    }

    void OnTriggerExit2D() {
        onUnpress.Raise();
    }
}
