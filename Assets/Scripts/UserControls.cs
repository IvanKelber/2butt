using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class UserControls : MonoBehaviour
{
    public GameEvent reset;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            reset.Raise();
        }
    }
}
