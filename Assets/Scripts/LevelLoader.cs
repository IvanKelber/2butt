using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public List<GameObject> levelPrefabs;

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
            Destroy(currentLevel);
            currentLevel = Instantiate(levelPrefabs[levelId], Vector3.zero, Quaternion.identity);
        } else {
            SceneManager.LoadScene(0);
        }
    }

}
