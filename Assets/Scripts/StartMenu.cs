using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public CanvasGroup instructions;
    public GameObject levelLoader;
    CanvasGroup startMenu;

    void Start() {
        startMenu = GetComponent<CanvasGroup>();
        HideInstructions();
    }

    public void StartGame() {
        startMenu.alpha = 0;
        levelLoader.SetActive(true);
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
