using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void LoadNextLevel()
    {
        string sceneName = "Scenes/Level" + (GLOBAL.currentLevel + 1).ToString();

        SceneManager.LoadScene(sceneName);
    }
}
