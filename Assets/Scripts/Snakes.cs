using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakes : MonoBehaviour
{
    public float fireRate;
    public float cooldown;
    // Start is called before the first frame update

    public AudioSource audioSource;
    void Start()
    {
        cooldown = 1 / fireRate * 60;
        audioSource = this.gameObject.GetComponent<AudioSource>();
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
        Enemy EnemyScript = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();
        if (EnemyScript.isPoisoned == true)
        {
            return;
        }
        EnemyScript.setPoison(8, 1f);
        cooldown =  1 / fireRate * 60;
        audioSource.Play();
    }
}
