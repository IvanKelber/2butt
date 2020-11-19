using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public CanvasGroup instructions;
    public GameObject levelLoader;

    LevelLoader loader;
    CanvasGroup startMenu;
    AudioSource audioSource;

    void Start() {
        startMenu = GetComponent<CanvasGroup>();
        loader = levelLoader.GetComponent<LevelLoader>();
        HideInstructions();
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    public void StartGame() {
        startMenu.alpha = 0;
        levelLoader.SetActive(true);
        loader.audioManager.Play("ButtonClick", audioSource);
    }

    public void ShowInstructions() {
        loader.audioManager.Play("ButtonClick", audioSource);

        instructions.alpha = 1;
        instructions.blocksRaycasts = true;
    }

    public void HideInstructions() {
        loader.audioManager.Play("ButtonClick", audioSource);
        instructions.alpha = 0;
        instructions.blocksRaycasts = false;
    }

}
