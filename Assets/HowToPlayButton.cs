using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayButton : MonoBehaviour
{
    public GameObject HowToPlayCanva;
    public GameObject CurCanva;

    public void HowToPlay()
    {
        HowToPlayCanva.SetActive(true);
        CurCanva.SetActive(false);
    }
}
