using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToScene : MonoBehaviour
{
    public int SceneIndex;
    public string sceneName;

    public void GoScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void GoSceneByName()
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
