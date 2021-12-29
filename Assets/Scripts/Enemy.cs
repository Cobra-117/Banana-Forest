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
                TileBase tile = tilemap.GetTile<TileBase>(new Vector3Int(tilemap.origin.x + i + 1, 
                tilemap.origin.y + j + 1, 0));
                if (tile.name == "dirt")
                {
                    Debug.Log("getting x: " + (tilemap.origin.x + i + 1).ToString() + "y: " + (tilemap.origin.y + j + 1).ToString());
                    Debug.Log(i + " " + j + " " + "dirt");
                }
                /*else
                    Debug.Log(i + " " + j + " " + "grass");*/
                distanceMap[j, i] = 9999;
            }
        }
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
            Debug.Log("x:" + cellpos.x + "y:" + cellpos.y);
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
}
