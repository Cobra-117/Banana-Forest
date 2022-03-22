using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public int level;

    private void Awake()
    {
        GLOBAL.score = 0;
        GLOBAL.currentLevel = level;
    }
}
