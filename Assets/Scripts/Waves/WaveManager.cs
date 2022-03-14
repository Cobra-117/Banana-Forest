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

    [Header("Unity")]
    public GameObject[] EnnemiesPrefab;
    public DistanceMap distanceMap;
    public Vector3 SpawnPosition;
    public GLOBAL global;

    public float countdown = 0;
    public float WaveCountdown = 30;
    public float breakCountdown = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPosition = distanceMap.tilemap.CellToWorld(distanceMap.distanceMapToTileMap(distanceMap.SpawnPosition));
        SpawnPosition = new Vector3(SpawnPosition.x - 0.5f, SpawnPosition.y + 0.32f, SpawnPosition.z);
        Debug.Log("spawn position :" + SpawnPosition.ToString());
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {
        if (global.health <= 0)
            Loose();
        countdown -= Time.deltaTime;
        if (breakCountdown > 0)
        {
            breakCountdown -= Time.deltaTime;
            return;
        }
        WaveCountdown -= Time.deltaTime;
        if (WaveCountdown <= 0)
        {
            breakCountdown = 20;
            global.curWave += 1;
            if ((global.curWave == 2 || global.curWave == 4 || global.curWave == 5) && difficulty < 10)
                difficulty += 1;
            WaveCountdown = 40;
            //if curWave >= maxWave && plus d'ennemis -> gagné
            return;
        }
        if (countdown <= 0)
        {
            countdown = SpawnCooldown + (10 - difficulty) * 0.15f ;
            SpawnEnemy();
            //if (Random.Range(1, 11 - difficulty) == 1)
            //SpawnEnemy();
        }
        
    }

    void SpawnEnemy()
    {
        GameObject Enemy = null;
        if (difficulty > 3 && global.curWave != 1 && Random.Range(0, (110 - (float)difficulty * 10) * 1.45f) < 10)
            Enemy = Instantiate(EnnemiesPrefab[Random.Range(1, 3)]);
        else
            Enemy = Instantiate(EnnemiesPrefab[0]);
        Enemy.transform.position = SpawnPosition;
    }

    void Loose()
    {
        SceneManager.LoadScene("Scenes/LostMenu");

    }
}
