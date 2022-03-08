using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManager : MonoBehaviour
{
    public int difficulty;
    public GameObject[] EnnemiesPrefab;
    public DistanceMap distanceMap;
    public Vector3 SpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPosition = distanceMap.tilemap.CellToWorld(distanceMap.distanceMapToTileMap(distanceMap.SpawnPosition));
        GameObject Enemy = Instantiate(EnnemiesPrefab[0]);
        Enemy.transform.position = SpawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
