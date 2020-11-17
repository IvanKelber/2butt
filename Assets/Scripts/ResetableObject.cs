using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(GameEventListener))]
public class ResetableObject : MonoBehaviour
{

    private Vector3 position;
    private Vector3 localScale;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Awake()
    {
        position = transform.position;
        localScale = transform.localScale;
        rotation = transform.rotation;
    }

    public void Reset() {
        this.transform.position = position;
        this.transform.localScale = localScale;
        this.transform.rotation = rotation;
    }
}
