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
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3Int TileMapTile = tilemap.WorldToCell(transform.position);
        Vector2Int DistanceMapTile = distanceMapScript.TileMapToDistanceMap(TileMapTile);
        int direction;

        direction = chooseMovingDirection(DistanceMapTile);
        switch (direction)
        {
            case 0:
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
                break;
            case 1:
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                break;
            case 2:
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
                break;
            case 3:
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
                break;
            default:
                Debug.Log("default");
                break;
        }

    }

    int chooseMovingDirection(Vector2Int pos)
    {
        int curDistance = -1;
        int bestDistance = 9999;
        int direction = -1;

        if (pos.y < 16)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y + 1, pos.x];
            if (curDistance < bestDistance)
            {
                direction = 0;
                bestDistance = curDistance;
            }
        }
        if (pos.x < 28)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y, pos.x + 1];
            if (curDistance < bestDistance)
            {
                direction = 1;
                bestDistance = curDistance;
            }
        }
        if (pos.y > 0)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y - 1, pos.x];
            if (curDistance < bestDistance)
            {
                direction = 2;
                bestDistance = curDistance;
            }
        }
        if (pos.x > 0)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y, pos.x - 1];
            if (curDistance < bestDistance)
            {
                direction = 3;
                bestDistance = curDistance;
            }
        }
        Debug.Log("current distance" + curDistance.ToString());
        Debug.Log("direction: " + direction.ToString());
        if (curDistance == 0)
            direction = -1;
        return direction;
    }
}

