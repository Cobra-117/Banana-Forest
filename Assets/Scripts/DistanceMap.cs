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
        Debug.Log(tilemap.localBounds);
        distanceMap = new int[tilemap.size.y, tilemap.size.x];
        Debug.Log("origin" + tilemap.origin.ToString());
        Debug.Log(tilemap.size.x.ToString());
        Debug.Log("size: " + tilemap.size.ToString());
        for (int j = 0; j < tilemap.size.y - 1; j++)
        {
            for (int i = 0; i < tilemap.size.x - 1; i++)
            {
                //Debug.Log("i: " + i.ToString());
                //Debug.Log("getting x: " + (tilemap.origin.x + i + 1).ToString() + "y: " + (tilemap.origin.y + j + 1).ToString());
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
        //Vector3 cellpos = tilemap.WorldToCell(transform.position);
        Vector3Int cellpos;
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //cellpos = new Vector3Int(0, 0, 0);
            cellpos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint((Input.mousePosition)));
            cellpos.z = 0;
            Vector2Int distCellPos = TileMapToDistanceMap(cellpos);
            Debug.Log("x:" + distCellPos.x + "y:" + distCellPos.y);
            TileBase tile = tilemap.GetTile<TileBase>(cellpos);
            if (tile != null)
            {
                Debug.Log("tile:" + tile.name);
            }
            else
            {
                Debug.Log(" tile: (null)");
            }
        }
        //Debug.Log("x: " + cellpos.x + "y: " + cellpos.y);   
    }

    private void SetDestination()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        int bestX = 0;

        TileBase BestTile = null;


        for (int j = 0; j < tilemap.size.y - 1; j++)
        {
            Debug.Log("J loop");
            for (int i = 0; i < tilemap.size.x - 1; i++)
            {
                Debug.Log("I loop");
                if (i < bestX)
                    continue; 
                TileBase tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i, j)));
                Debug.Log("y : " + j.ToString());
                if (tile == null)
                {
                    Debug.Log("null tile");
                    continue;
                }
                Debug.Log("name : " + tile.name);
                if (tile.name.Contains("path"))
                {
                    Debug.Log("found!");
                    Destination = new Vector2Int(i - 1, j);
                    bestX = i;
                    //return;
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
        //Debug.Log("dist map dest " + distanceMap[8, 27].ToString());

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
                    /*Debug.Log("dist map " + i + " " + j + ""
                        + "is indexed");*/

                    /*indexing x -1 tile*/
                    tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i - 1, j)));
                    if (tile != null && tile.name.Contains("path") && distanceMap[j, i - 1] == 9999)
                    {
                        distanceMap[j, i - 1] = CurrentDistance + 1;
                        indexedTiles = true;
                        SpawnPosition = new Vector2Int(i, j);
                        //Debug.Log("indexed x - 1");
                    }

                    /*indexing x + 1 tile*/
                    tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i + 1, j)));
                    if (tile != null && tile.name.Contains("path") && distanceMap[j, i + 1] == 9999)
                    {
                        distanceMap[j, i + 1] = CurrentDistance + 1;
                        indexedTiles = true;
                        SpawnPosition = new Vector2Int(i, j);
                        Debug.Log("indexed x + 1");
                    }

                    /*indexing y -1 tile*/
                    tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i, j - 1)));
                    if (tile != null && tile.name.Contains("path") && distanceMap[j - 1, i] == 9999)
                    {
                        distanceMap[j - 1, i] = CurrentDistance + 1;
                        indexedTiles = true;
                        SpawnPosition = new Vector2Int(i, j);
                        //Debug.Log("indexed y - 1");
                    }

                    /*indexing y + 1 tile*/
                    tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i, j + 1)));
                    if (tile != null && tile.name.Contains("path") && distanceMap[j + 1, i] == 9999)
                    {
                        distanceMap[j + 1, i] = CurrentDistance + 1;
                        indexedTiles = true;
                        SpawnPosition = new Vector2Int(i, j);
                        //Debug.Log("indexed y + 1");
                    }
                    //TileBase tile = tilemap.GetTile<TileBase>(distanceMap);
                }
            }
            CurrentDistance++;
        }
        Debug.Log("total dist: " + CurrentDistance.ToString());
    }
}
