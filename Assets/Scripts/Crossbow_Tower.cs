using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow_Tower : MonoBehaviour
{
    public float range;
    public float fireRate;
    public GameObject projectilePrefab;
    public GameObject currentProjectile;

    private GameObject closestEnemy;
    private float fireCoutdown;
    private bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        closestEnemy = find_closest_enemy();
        fireCoutdown = 1 / fireRate * 60;
    }

    // Update is called once per frame
    void Update()
    {
        fireCoutdown -= Time.deltaTime;
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
        if (currentProjectile == null)
            createProjectile();
        if (debug == true)
            return;
        Projectile proj = currentProjectile.GetComponent<Projectile>();

        proj.speed = 10;
        transform.DetachChildren();
        Debug.Log("Boom");
        debug = true;
    }

    void createProjectile()
    {
        currentProjectile = Instantiate(projectilePrefab, transform);
    }
}
