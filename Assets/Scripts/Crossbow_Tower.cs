using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow_Tower : MonoBehaviour
{
    [Header("Stats")]
    public float range;
    public float fireRate;

    [Header("Unity")]
    public GameObject projectilePrefab;
    public GameObject currentProjectile;
    public Sprite HighSprite;
    public Sprite LowSprite;

    private SpriteRenderer _SpriteRenderer;
    private GameObject closestEnemy;
    private float fireCoutdown;
    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        closestEnemy = find_closest_enemy();
        fireCoutdown = 1 / fireRate * 60;
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _SpriteRenderer.sprite = HighSprite;
    }

    // Update is called once per frame
    void Update()
    {
        fireCoutdown -= Time.deltaTime;
        if (_SpriteRenderer.sprite == LowSprite && fireCoutdown < (1 / fireRate * 60) / 2)
        {
            _SpriteRenderer.sprite = HighSprite;
            createProjectile();
        }
        if (closestEnemy == null || DistToObj(closestEnemy) > range)
            closestEnemy = find_closest_enemy();
        if (closestEnemy != null)
        {
            lookAtEnemy(closestEnemy);
            if (fireCoutdown <= 0)
            {
                Shoot();
                fireCoutdown = 1 / fireRate * 60;
            }
        }
            //transform.LookAt( new Vector3(closestEnemy.transform.position.x, closestEnemy.transform.position.y, 0));
    }

    void lookAtEnemy(GameObject Enemy)
    {
        Vector3 v_diff = (Enemy.transform.position - transform.position);
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg +180);
    }

    public GameObject find_closest_enemy()
    {
        GameObject closest = null;
        GameObject[] ennemies = null;
        Vector3 local_pos = transform.position;
        float bs_dist = Mathf.Infinity;
        float cur_dist = 0;

        ennemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in ennemies)
        {
            Debug.Log("Ennemy");
            cur_dist = DistToObj(enemy);
            if (cur_dist < bs_dist && cur_dist <= range)
            {
                bs_dist = cur_dist;
                closest = enemy;
            }
        }
        return (closest);
    }

    public float DistToObj(GameObject obj)
    {
        float dist = 0;
        float dist_x = 0;
        float dist_y = 0;

        dist_x = transform.position.x - obj.transform.position.x;
        dist_y = transform.position.y - obj.transform.position.y;
        if (dist_x < 0)
            dist_x = dist_x * (-1);
        if (dist_y < 0)
            dist_y = dist_y * (-1);
        dist = (dist_x + dist_y);
        return (dist);
    }

    public void Shoot()
    {
        Projectile proj = currentProjectile.GetComponent<Projectile>();

        proj.speed = 10;
        transform.DetachChildren();
        _SpriteRenderer.sprite = LowSprite;
    }

    void createProjectile()
    {
        if (currentProjectile != null)
            return;
        currentProjectile = Instantiate(projectilePrefab, transform);
    }
}
