using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public GameObject pauseManager;
    public PauseGame pauseGame;
    // Start is called before the first frame update
    void Start()
    {
        pauseManager = transform.parent.gameObject.transform.parent.gameObject;
        pauseGame = pauseManager.GetComponent<PauseGame>();
    }

    public void OnClick()
    {
        pauseGame.Resume();
    }
}
