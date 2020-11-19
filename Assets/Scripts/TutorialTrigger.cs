using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField]
    CanvasGroup tutorialPanel;

    void Start() {
        tutorialPanel.alpha = 0;
    }

    void OnTriggerEnter2D() {
        tutorialPanel.alpha = 1;
    }

    public void HideTutorialInstruction() {
        tutorialPanel.alpha = 0;
        Destroy(gameObject);
    }

}
