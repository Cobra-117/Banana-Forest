using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int life = 10;
    public DistanceMap distanceMapScript;
    public int[,] _distanceMap;
    public Tilemap tilemap;
    public GameObject _sprite;
    Vector2Int currentFinishedTile;

    void Start()
    {
        _distanceMap = distanceMapScript.distanceMap;
        tilemap = distanceMapScript.tilemap;
        Vector3Int TileMapTile = tilemap.WorldToCell(transform.position);
        Vector2Int DistanceMapTile = distanceMapScript.TileMapToDistanceMap(TileMapTile);
        currentFinishedTile = new Vector2Int(DistanceMapTile.x -1, DistanceMapTile.y - 1);
    }

    void Update()
    {
        if (life == 0)
            Destroy(this.gameObject);
        Move();
    }

    void Move()
    {
        Vector3Int TileMapTile = tilemap.WorldToCell(transform.position);
        Vector2Int DistanceMapTile = distanceMapScript.TileMapToDistanceMap(TileMapTile);
        int direction = -1;

        if (FinishTile(DistanceMapTile) == 0)
            direction = chooseMovingDirection(DistanceMapTile);
        if (direction != -1)
        {
            switch (direction)
            {
                case 0:
                    transform.Translate(new Vector3(0, speed * Time.deltaTime, 0), Space.World);
                    currentFinishedTile.y += 1;
                    _sprite.transform.eulerAngles = new Vector3(_sprite.transform.eulerAngles.x, _sprite.transform.eulerAngles.y, -90);
                    break;
                case 1:
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.World);
                    currentFinishedTile.x += 1;
                    _sprite.transform.eulerAngles = new Vector3(_sprite.transform.eulerAngles.x, _sprite.transform.eulerAngles.y, 180);
                    break;
                case 2:
                    transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0), Space.World);
                    _sprite.transform.eulerAngles = new Vector3(_sprite.transform.eulerAngles.x, _sprite.transform.eulerAngles.y, 90);
                    currentFinishedTile.y -= 1;
                    break;
                case 3:
                    transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0), Space.World);
                    _sprite.transform.eulerAngles = new Vector3(_sprite.transform.eulerAngles.x,  _sprite.transform.eulerAngles.y, 0);
                    currentFinishedTile.x -= 1;
                    break;
                default:
                    //Debug.Log("default");
                    break;
            }
            //Vérifier si on a changé de tile
        }

    }

    int FinishTile(Vector2Int pos)
    {
        Vector2 TileCenter = new Vector2(-8.64f + (currentFinishedTile.x + 1) * 0.64f, -4.8f + (currentFinishedTile.y + 1) * 0.64f); //set la valeur
        //Debug.Log("tilepos :" + pos);
        //Debug.Log("tilecenter :" + TileCenter.ToString());
        if (transform.position.y < TileCenter.y)
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0), Space.World);
            if (transform.position.y > TileCenter.y)
                transform.position = new Vector3(transform.position.x, TileCenter.y, transform.position.z);
            return (1);
        }
        if (transform.position.x < TileCenter.x)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.World);
            if (transform.position.x > TileCenter.x)
                transform.position = new Vector3(TileCenter.x, transform.position.y, transform.position.z);
            return (1);
        }
        if (transform.position.y > TileCenter.y)
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0), Space.World);
            if (transform.position.y < TileCenter.y)
                transform.position = new Vector3(transform.position.x, TileCenter.y, transform.position.z);
            return (1);
        }
        if (transform.position.x > TileCenter.x)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0), Space.World);
            if (transform.position.x < TileCenter.x)
                transform.position = new Vector3(TileCenter.x, transform.position.y, transform.position.z);
            return (1);
        }
        return (0);
        //Debug.Log("object pos:" + transform.position);
    }

    int chooseMovingDirection(Vector2Int pos)
    {
        int curDistance = -1;
        int bestDistance = 9999;
        int direction = -1;

        //Debug.Log("pos : " + pos.ToString());
        if (pos.y < 16)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[0, 0];
            curDistance = _distanceMap[pos.y + 1, pos.x];
            if (curDistance < bestDistance)
            {
                //Debug.Log("current distance0 " + curDistance.ToString());
                direction = 0;
                bestDistance = curDistance;
            }
        }
        if (pos.x < 28)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y, pos.x + 1];
            if (curDistance < bestDistance)
            {
                //Debug.Log("current distance1 " + curDistance.ToString());
                direction = 1;
                bestDistance = curDistance;
            }
        }
        if (pos.y > 0)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y - 1, pos.x];
            if (curDistance < bestDistance)
            {
                //Debug.Log("current distance2 " + curDistance.ToString());
                direction = 2;
                bestDistance = curDistance;
            }
        }
        if (pos.x > 0)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y, pos.x - 1];
            if (curDistance < bestDistance)
            {
                //Debug.Log("current distance3 " + curDistance.ToString());
                direction = 3;
                bestDistance = curDistance;
            }
        }
        //Debug.Log("direction: " + direction.ToString());
        if (curDistance == 0)
            direction = -1;
        return direction;
    }
}

