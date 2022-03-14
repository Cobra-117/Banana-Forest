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
        Debug.Log("Trigger entered");
        if (collision.gameObject.tag != "Enemy" || cooldown > 0)
            return;
        Enemy EnemyScript = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();
        if (EnemyScript.isPoisoned == true)
        {
            Debug.Log("alreay poisonned");
            return;
        }
        EnemyScript.setPoison(8, 1f);
        cooldown =  1 / fireRate * 60;
    }
}
