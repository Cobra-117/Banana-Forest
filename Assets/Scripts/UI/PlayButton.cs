using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject LevelSelectionCanva;
    public GameObject CurCanva;

    public void Play()
    {
        LevelSelectionCanva.SetActive(true);
        CurCanva.SetActive(false);
    }
}
