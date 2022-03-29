using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaTower : MonoBehaviour
{
    [Header("Stats")]
    public float range;
    public float fireRate;

    [Header("Unity")]
    public GameObject projectilePrefab;
    private GameObject closestEnemy = null;

    private float fireCoutdown;

    // Start is called before the first frame update
    void Start()
    {
        closestEnemy = find_closest_enemy();
    }

    // Update is called once per frame
    void Update()
    {
        fireCoutdown -= Time.deltaTime;
        if (closestEnemy == null || DistToObj(closestEnemy) > range)
            closestEnemy = find_closest_enemy();
        if (closestEnemy != null)
        {
            if (fireCoutdown <= 0)
            {
                Shoot();
                fireCoutdown = 1 / fireRate * 60;
            }
        }
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
        GameObject banana = Instantiate(projectilePrefab);
        BananaProjectile script = banana.GetComponent<BananaProjectile>();

        banana.transform.position = this.gameObject.transform.position;
        script.Target = closestEnemy;
        script.lookAtEnemy(script.Target);
        script.speed = 8;
        script.range = range;
    }
}
