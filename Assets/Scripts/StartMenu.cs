using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public CanvasGroup instructions;

    void Start() {
        HideInstructions();
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ShowInstructions() {
        instructions.alpha = 1;
        instructions.blocksRaycasts = true;
    }

    public void HideInstructions() {
        instructions.alpha = 0;
        instructions.blocksRaycasts = false;
    }

}
