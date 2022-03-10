using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    public GameObject previousMenu;
    public GameObject curCanva;

    public void Go_Back()
    {
        previousMenu.SetActive(true);
        curCanva.SetActive(false);
    }
}
