using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MecanoBulldozer : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float MaxHealth = 10;

    [Header("Unity")]
    public DistanceMap distanceMapScript;
    public int[,] _distanceMap;
    public Tilemap tilemap;
    public GameObject _sprite;
    public GameObject HealthBar;
    public bool isPoisoned;
    public float PoisonPower;
    public float poisonCoutdown = 0;

    public float curHealth;
    Vector2Int currentFinishedTile;

    void Start()
    {
        distanceMapScript = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<DistanceMap>();
        _distanceMap = distanceMapScript.distanceMap;
        tilemap = distanceMapScript.tilemap;
        Vector3Int TileMapTile = tilemap.WorldToCell(transform.position);
        Vector2Int DistanceMapTile = distanceMapScript.TileMapToDistanceMap(TileMapTile);
        currentFinishedTile = new Vector2Int(DistanceMapTile.x - 1, DistanceMapTile.y - 1);
        curHealth = MaxHealth;
    }

    void Update()
    {
        if (isPoisoned == true)
        {
            curHealth -= PoisonPower * Time.deltaTime;
            poisonCoutdown -= Time.deltaTime;
            if (poisonCoutdown < 0)
                stopPoison();
        }
        if (curHealth <= 0)
            Destroy(this.gameObject);
        HealthBar.transform.localScale = new Vector3(0.4f / MaxHealth * curHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
        Move();
    }

    public void setPoison(float duration, float power)
    {
        isPoisoned = true;
        if (duration > poisonCoutdown)
            poisonCoutdown = duration;
        if (power > PoisonPower)
            PoisonPower = power;
        _sprite.GetComponent<SpriteRenderer>().color = new Color(0.5877653f, 0.9150943f, 0.4445977f, 1);
    }

    public void stopPoison()
    {
        isPoisoned = false;
        poisonCoutdown = 0;
        PoisonPower = 0;
        _sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
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
                    _sprite.transform.eulerAngles = new Vector3(_sprite.transform.eulerAngles.x, _sprite.transform.eulerAngles.y, 0);
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
        int bestDistance = 9999;
        int direction = -1;

        if (pos.y < tilemap.size.y - 1)
        {
            curDistance = _distanceMap[0, 0];
            curDistance = _distanceMap[pos.y + 1, pos.x];
            if (curDistance < bestDistance)
            {
                direction = 0;
                bestDistance = curDistance;
            }
        }
        if (pos.x < tilemap.size.x - 1)
        {
            curDistance = _distanceMap[pos.y, pos.x + 1];
            if (curDistance < bestDistance)
            {
                direction = 1;
                bestDistance = curDistance;
            }
        }
        if (pos.y > 0)
        {
            curDistance = _distanceMap[pos.y - 1, pos.x];
            if (curDistance < bestDistance)
            {
                direction = 2;
                bestDistance = curDistance;
            }
        }
        if (pos.x > 0)
        {
            curDistance = _distanceMap[pos.y, pos.x - 1];
            if (curDistance < bestDistance)
            {
                direction = 3;
                bestDistance = curDistance;
            }
        }
        if (curDistance == 0)
            direction = -1;
        return direction;
    }
}
