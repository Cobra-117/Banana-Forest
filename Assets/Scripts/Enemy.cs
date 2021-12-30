using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public Tilemap tilemap;
    private Vector3 pos;
    private int[,] distanceMap;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(tilemap.localBounds);
        distanceMap = new int[tilemap.size.y, tilemap.size.x];
        Debug.Log("origin" + tilemap.origin.ToString());
        Debug.Log(tilemap.size.x.ToString());
        for (int j = 0; j < tilemap.size.y - 1; j++)
        {
            for (int i = 0; i < tilemap.size.x - 1; i++)
            {
                //Debug.Log("i: " + i.ToString());
                //Debug.Log("getting x: " + (tilemap.origin.x + i + 1).ToString() + "y: " + (tilemap.origin.y + j + 1).ToString());
                TileBase tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i, j)));
                if (tile.name == "dirt")
                {
                    Debug.Log("getting x: " + (tilemap.origin.x + i + 1).ToString() + "y: " + (tilemap.origin.y + j + 1).ToString());
                    Debug.Log(i + " " + j + " " + "dirt");
                }
                distanceMap[j, i] = 9999;
            }
        }
        indexDistanceMap(new Vector3Int(27, 8, 0));
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

    Vector3Int distanceMapToTileMap(Vector2Int coords)
    {
        Vector3Int res = new Vector3Int(0, 0, 0);
        res.x = coords.x + 1 + tilemap.origin.x;
        res.y = coords.y + 1 + tilemap.origin.y;
        return res;
    }

    Vector2Int TileMapToDistanceMap(Vector3Int coords)
    {
        Vector2Int res = new Vector2Int(0, 0);
        res.x = coords.x - (1 + tilemap.origin.x);
        res.y = coords.y - (1 + tilemap.origin.y);
        return res;
    }

    void indexDistanceMap(Vector3Int destination)
    {
        int curdist = 0;
        bool hasFoundDirt = true;
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        distanceMap[destination.y, destination.x] = 0;
        Debug.Log("dist map dest " + distanceMap[8, 27].ToString());
        while (hasFoundDirt == true)
        {
            hasFoundDirt = false;
            for (int j = 0; j < tilemap.size.y - 1; j++)
            {
                for (int i = 0; i < tilemap.size.x - 1; i++)
                {
                    TileBase tile = tilemap.GetTile<TileBase>(distanceMapToTileMap(new Vector2Int(i, j)));
                    /*if tile is not indexed yet, continue*/
                    if (tile.name != "dirt" || distanceMap[j, i] == 9999)
                        continue;
                    Debug.Log("dist map " + i + " " + j + ""
                        + "is indexed");

                    /*indexing y -1 tile*/
                    //distanceMap[]
                    //TileBase tile = tilemap.GetTile<TileBase>(distanceMap);
                }
            }
        }
    }
}

