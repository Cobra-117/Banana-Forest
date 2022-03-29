using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DistanceMap : MonoBehaviour
{
    public Tilemap tilemap;
    private Vector3 pos;
    public int[,] distanceMap;
    public bool isInit = false;
    public Vector2Int Destination;
    public Vector2Int SpawnPosition;


    // Start is called before the first frame update
    void Awake()
    {
        distanceMap = new int[tilemap.size.y, tilemap.size.x];
        for (int j = 0; j < tilemap.size.y - 1; j++)
        {
            for (int i = 0; i < tilemap.size.x - 1; i++)
            {
                distanceMap[j, i] = 9999;
            }
        }
        SetDestination();
        indexDistanceMap(new Vector3Int(Destination.x, Destination.y, 0));
        isInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int cellpos;
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            cellpos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint((Input.mousePosition)));
            cellpos.z = 0;
            Vector2Int distCellPos = TileMapToDistanceMap(cellpos);
            TileBase tile = tilemap.GetTile<TileBase>(cellpos);
        }  
    }

    private void SetDestination()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        int bestX = 0;

        TileBase BestTile = null;


        for (int j = 0; j < tilemap.size.y - 1; j++)
        {
            for (int i = 0; i < tilemap.size.x - 1; i++)
            {
                if (i < bestX)
                    continue; 
                TileBase tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i, j)));
                if (tile == null)
                {
                    continue;
                }
                if (tile.name.Contains("path"))
                {
                    Destination = new Vector2Int(i - 1, j);
                    bestX = i;
                }
            }
        }
    }

    public Vector3Int distanceMapToTileMap(Vector2Int coords)
    {
        Vector3Int res = new Vector3Int(0, 0, 0);
        res.x = coords.x + 1 + tilemap.origin.x;
        res.y = coords.y + 1 + tilemap.origin.y;
        return res;
    }

    public Vector2Int TileMapToDistanceMap(Vector3Int coords)
    {
        Vector2Int res = new Vector2Int(0, 0);
        res.x = coords.x - (1 + tilemap.origin.x);
        res.y = coords.y - (1 + tilemap.origin.y);
        return res;
    }

    void indexDistanceMap(Vector3Int destination)
    {
        int CurrentDistance = 0;
        bool indexedTiles = true;
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        distanceMap[destination.y, destination.x] = 0;


        /*next level of distance*/
        while (indexedTiles == true)
        {
            indexedTiles = false;
            /*indexing each tile adjacent to CurrentDistance tiles*/
            for (int j = 0; j < tilemap.size.y - 1; j++)
            {
                for (int i = 0; i < tilemap.size.x - 1; i++)
                {
                    TileBase tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i, j)));
                    /*if tile distance is not equal to CurrentDistance, continue*/
                    if (tile == null || !tile.name.Contains("path") || distanceMap[j, i] != CurrentDistance)
                        continue;
                    tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i - 1, j)));
                    if (tile != null && tile.name.Contains("path") && distanceMap[j, i - 1] == 9999)
                    {
                        distanceMap[j, i - 1] = CurrentDistance + 1;
                        indexedTiles = true;
                        SpawnPosition = new Vector2Int(i, j);
                    }

                    tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i + 1, j)));
                    if (tile != null && tile.name.Contains("path") && distanceMap[j, i + 1] == 9999)
                    {
                        distanceMap[j, i + 1] = CurrentDistance + 1;
                        indexedTiles = true;
                        SpawnPosition = new Vector2Int(i, j);
                    }

                    /*indexing y -1 tile*/
                    tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i, j - 1)));
                    if (tile != null && tile.name.Contains("path") && distanceMap[j - 1, i] == 9999)
                    {
                        distanceMap[j - 1, i] = CurrentDistance + 1;
                        indexedTiles = true;
                        SpawnPosition = new Vector2Int(i, j);
                    }

                    /*indexing y + 1 tile*/
                    tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i, j + 1)));
                    if (tile != null && tile.name.Contains("path") && distanceMap[j + 1, i] == 9999)
                    {
                        distanceMap[j + 1, i] = CurrentDistance + 1;
                        indexedTiles = true;
                        SpawnPosition = new Vector2Int(i, j);
                    }
                }
            }
            CurrentDistance++;
        }
    }
}
