using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class ButtonPress : MonoBehaviour
{
    BoxCollider2D collider;

    [SerializeField]
    GameEvent onPress, onUnpress;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }


    void OnTriggerEnter2D() {
        Debug.Log("Entering");
        onPress.Raise();
    }

    void OnTriggerStay2D() {
        onPress.Raise();
    }

    void OnTriggerExit2D() {
        Debug.Log("Exiting");
        onUnpress.Raise();
    }
}
