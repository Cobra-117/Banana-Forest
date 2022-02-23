using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakes : MonoBehaviour
{
    public float fireRate;
    public float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 1 / fireRate * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" || cooldown > 0)
            return;
        Enemy EnemyScript = collision.gameObject.GetComponent<Enemy>();
        EnemyScript.setPoison(3, 1);
        cooldown =  1 / fireRate * 60;
    }
}
