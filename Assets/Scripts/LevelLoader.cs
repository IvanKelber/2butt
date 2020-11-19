using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public List<GameObject> levelPrefabs;

    public CanvasGroup canvas;

    public Fade fade;
    int levelId = 0;
    GameObject currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = Instantiate(levelPrefabs[0], Vector3.zero, Quaternion.identity);
    }

    public void LoadNextLevel() {
        levelId++;
        if(levelId < levelPrefabs.Count) {
            StartCoroutine(LoadLevel(levelId));
        } else {
            StartCoroutine(LoadStartMenu());
        }
    }

    IEnumerator LoadStartMenu() {
        if(fade != null) {
            Debug.Log("fading out");
            yield return StartCoroutine(fade.FadeOut());
        }
        Destroy(currentLevel);
        canvas.alpha = 1;
        if(fade != null) {
            Debug.Log("Fading in");
            yield return StartCoroutine(fade.FadeInInverted());
        }
    }

    IEnumerator LoadLevel(int levelId) {
        if(fade != null) {
            Debug.Log("fading out");
            yield return StartCoroutine(fade.FadeOut());
        }
        Destroy(currentLevel);
        currentLevel = Instantiate(levelPrefabs[levelId], Vector3.zero, Quaternion.identity);
        if(fade != null) {
            Debug.Log("Fading in");
            yield return StartCoroutine(fade.FadeInInverted());
        }
    }

    public void Reset() {
        StartCoroutine(LoadLevel(levelId));
    }

}
