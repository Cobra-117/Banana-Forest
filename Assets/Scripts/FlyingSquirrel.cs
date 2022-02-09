using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlyingSquirrel : MonoBehaviour
{
    public Vector3 MapEnd;
    public DistanceMap distanceMapScript;
    public int[,] _distanceMap;
    Vector2Int currentFinishedTile;
    public Tilemap tilemap;
    public float speed;
    public GameObject _sprite;


    // Start is called before the first frame update
    void Start()
    {
        distanceMapScript = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<DistanceMap>();
        _distanceMap = distanceMapScript.distanceMap;
        tilemap = distanceMapScript.tilemap;
        Vector3Int TileMapTile = tilemap.WorldToCell(transform.position);
        Vector2Int DistanceMapTile = distanceMapScript.TileMapToDistanceMap(TileMapTile);
        currentFinishedTile = new Vector2Int(DistanceMapTile.x - 1, DistanceMapTile.y - 1);
    }

    // Update is called once per frame
    void Update()
    {
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
                    _sprite.transform.eulerAngles = new Vector3(_sprite.transform.eulerAngles.x, _sprite.transform.eulerAngles.y, 0);
                    break;
                case 1:
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.World);
                    currentFinishedTile.x += 1;
                    _sprite.transform.eulerAngles = new Vector3(_sprite.transform.eulerAngles.x, _sprite.transform.eulerAngles.y, -90);
                    break;
                case 2:
                    transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0), Space.World);
                    _sprite.transform.eulerAngles = new Vector3(_sprite.transform.eulerAngles.x, _sprite.transform.eulerAngles.y, 180);
                    currentFinishedTile.y -= 1;
                    break;
                case 3:
                    transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0), Space.World);
                    _sprite.transform.eulerAngles = new Vector3(_sprite.transform.eulerAngles.x, _sprite.transform.eulerAngles.y, 90);
                    currentFinishedTile.x -= 1;
                    break;
                default:
                    break;
            }
        }

    }

    int FinishTile(Vector2Int pos)
    {
        Vector2 TileCenter = new Vector2(-8.64f + (currentFinishedTile.x + 1) * 0.64f, -4.8f + (currentFinishedTile.y + 1) * 0.64f); //set la valeur
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
    }

    int chooseMovingDirection(Vector2Int pos)
    {
        int curDistance = -1;
        int bestDistance = -1;
        int direction = -1;

        if (pos.y < 16)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[0, 0];
            curDistance = _distanceMap[pos.y + 1, pos.x];
            if (curDistance != 9999 && curDistance > bestDistance)
            {
                direction = 0;
                bestDistance = curDistance;
            }
        }
        if (pos.x < 28)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y, pos.x + 1];
            if (curDistance != 9999 && curDistance > bestDistance)
            {
                direction = 1;
                bestDistance = curDistance;
            }
        }
        if (pos.y > 0)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y - 1, pos.x];
            if (curDistance != 9999 && curDistance > bestDistance)
            {
                direction = 2;
                bestDistance = curDistance;
            }
        }
        if (pos.x > 0)
        {//set un truc qui marche pour tout les tailles de map
            curDistance = _distanceMap[pos.y, pos.x - 1];
            if (curDistance != 9999 && curDistance > bestDistance)
            {
                direction = 3;
                bestDistance = curDistance;
            }
        }
        if (curDistance == 0)
            direction = -1;
        return direction;
    }


    //Suit la map à l'envers
}
