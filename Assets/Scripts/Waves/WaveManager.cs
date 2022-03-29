using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    public int difficulty;
    public float SpawnCooldown;
    public bool Endless;

    [Header("Unity")]
    public GameObject[] EnnemiesPrefab;
    public DistanceMap distanceMap;
    public Vector3 SpawnPosition;
    public GLOBAL global;

    public float BeginCountdown = 3;
    public float countdown = 0;
    public float WaveCountdown = 50;
    public float breakCountdown = 100;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPosition = distanceMap.tilemap.CellToWorld(distanceMap.distanceMapToTileMap(distanceMap.SpawnPosition));
        SpawnPosition = new Vector3(SpawnPosition.x - 0.5f, SpawnPosition.y + 0.32f, SpawnPosition.z);
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {
        if (BeginCountdown > 0)
        {
            BeginCountdown -= Time.deltaTime;
            return;
        }
        if (global.health <= 0)
            Loose();
        countdown -= Time.deltaTime;
        if (WaveCountdown <= 0 && breakCountdown > 0 && breakCountdown != 100)
        {
            breakCountdown -= Time.deltaTime;

        }
        WaveCountdown -= Time.deltaTime;
        if (WaveCountdown <= 0)
        {
            if (Endless == false && global.curWave == 5 && GameObject.FindGameObjectWithTag("Enemy") == null) {
                if (GLOBAL.currentLevel == 6)
                    SceneManager.LoadScene("Scenes/FinalMenu");
                else 
                    SceneManager.LoadScene("Scenes/WonMenu");
            }
            if (breakCountdown == 100)
            {
                breakCountdown = 20;
            }
            if (breakCountdown > 0)
                return;
            if ((Endless == true || global.curWave < 5))
            {
                global.curWave += 1;
            }
            breakCountdown = 100;
            if (Endless == false && (global.curWave == 2 || global.curWave == 4 || global.curWave == 5) && difficulty < 10)
            {
                difficulty += 1;
            }
            else if (difficulty < 10)
            {
                difficulty += 1;
            }
            if (global.curWave == 5 || (global.curWave > 5 && Endless == true))
                WaveCountdown = 70;
            else
                WaveCountdown = 50;
            return;
        }
        if (countdown <= 0)
        {
            countdown = SpawnCooldown + (10 - difficulty) * 0.15f ;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject Enemy = null;
        if (difficulty == 10)
        {
            if (Random.Range(1, 4) != 1)
                 Enemy = Instantiate(EnnemiesPrefab[Random.Range(1, 3)]);
             else
                 Enemy = Instantiate(EnnemiesPrefab[0]);
            Enemy.transform.position = SpawnPosition;
        }
        else
        {
            if (difficulty > 3 && global.curWave != 1 && Random.Range(0, (110 - (float)difficulty * 10) * 1.45f) < 10)
                Enemy = Instantiate(EnnemiesPrefab[Random.Range(1, 3)]);
            else
                Enemy = Instantiate(EnnemiesPrefab[0]);
            Enemy.transform.position = SpawnPosition;
        }
    }

    void Loose()
    {
        SceneManager.LoadScene("Scenes/LostMenu");
    }
}
