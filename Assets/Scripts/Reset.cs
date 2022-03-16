using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public int level;
    public AudioSource audio;

    private void Awake()
    {
        GLOBAL.score = 0;
        GLOBAL.currentLevel = level;
    }

    private void Start()
    {
        audio.PlayOneShot((AudioClip)Resources.Load("Sounds/ForwardBird"));
    }
}
