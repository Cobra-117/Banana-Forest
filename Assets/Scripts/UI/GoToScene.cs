using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToScene : MonoBehaviour
{
    public int SceneIndex;

    public void GoScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
