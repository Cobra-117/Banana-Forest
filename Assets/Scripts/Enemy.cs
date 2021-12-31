using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public float speed;
    public DistanceMap distanceMapScript;
    public int[,] _distanceMap;
    public Tilemap tilemap;

    void Start()
    {
        _distanceMap = distanceMapScript.distanceMap;
        tilemap = distanceMapScript.tilemap;
        Move();
    }

    void Update()
    {
        
    }

    void Move()
    {
        Vector3Int TileMapTile = tilemap.WorldToCell(transform.position);
        Vector2Int DistanceMapTile = distanceMapScript.TileMapToDistanceMap(TileMapTile);

        Debug.Log("TilemapTile: " + TileMapTile.ToString());
        Debug.Log("DistanceMapTile: " + DistanceMapTile.ToString());
        Debug.Log("Distance: " + _distanceMap[DistanceMapTile.y, DistanceMapTile.x]);
        //Vector2Int curTile = _distanceMap.
    }
}

