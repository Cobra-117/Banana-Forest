using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    public int difficulty;
    public float SpawnCooldown;

    [Header("Unity")]
    public GameObject[] EnnemiesPrefab;
    public DistanceMap distanceMap;
    public Vector3 SpawnPosition;

    private float countdown = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPosition = distanceMap.tilemap.CellToWorld(distanceMap.distanceMapToTileMap(distanceMap.SpawnPosition));
        SpawnPosition = new Vector3(SpawnPosition.x - 0.5f, SpawnPosition.y + 0.32f, SpawnPosition.z);
        Debug.Log("spawn position :" + SpawnPosition.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = SpawnCooldown;
            if (Random.Range(1, 10 - difficulty) == 1)
                SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject Enemy = Instantiate(EnnemiesPrefab[0]);
        Enemy.transform.position = SpawnPosition;
    }
}