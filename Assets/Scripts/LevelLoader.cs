using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public List<GameObject> levelPrefabs;

    public CanvasGroup canvas;
    public AudioManager audioManager;
    public Fade fade;

    AudioSource audioSource;
    int levelId = -1;
    GameObject currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        LoadNextLevel();
    }

    public void LoadNextLevel() {
        levelId++;
        audioManager.Play("Transition", audioSource);
        if(levelId < levelPrefabs.Count) {
            StartCoroutine(LoadLevel(levelId));
        } else {
            StartCoroutine(LoadStartMenu());
        }
    }

    IEnumerator LoadStartMenu() {
        if(fade != null) {
            yield return StartCoroutine(fade.FadeOut());
        }
        Destroy(currentLevel);
        canvas.alpha = 1;
        if(fade != null) {
            yield return StartCoroutine(fade.FadeInInverted());
        }
    }

    IEnumerator LoadLevel(int levelId) {
        if(fade != null) {
            yield return StartCoroutine(fade.FadeOut());
        }
        Destroy(currentLevel);
        currentLevel = Instantiate(levelPrefabs[levelId], Vector3.zero, Quaternion.identity);
        if(fade != null) {
            yield return StartCoroutine(fade.FadeInInverted());
        }
    }

    public void Reset() {
        audioManager.Play("Transition", audioSource);
        StartCoroutine(LoadLevel(levelId));
    }

}
