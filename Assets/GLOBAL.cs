using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL : MonoBehaviour
{
    public int money = 0;
    public short health = 10;
    public short curWave = 1;
    public short maxWave;
    public int score;

    private float ScoreUpdateCoutdown = 0.1f;

    private void Update()
    {
        ScoreUpdateCoutdown -= Time.deltaTime;
        if (ScoreUpdateCoutdown <= 0)
        {
            score += 1;
            ScoreUpdateCoutdown = 0.3f;
        }
    }
}
