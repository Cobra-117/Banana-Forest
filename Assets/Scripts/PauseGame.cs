using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool IsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape) == true)
       {
            if (IsPaused == false)
                Pause();
            else
                Resume();
       } 
    }

    void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
