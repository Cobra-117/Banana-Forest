using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 1;

    private float countDown = 5;
    private bool hasTouched = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (speed != 0)
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        if (countDown <= 0)
            Destroy(this.gameObject);
        if (hasTouched == true && !audioSource.isPlaying)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasTouched == true)
            return;
        Debug.Log("collision");
        if (collision.gameObject.tag == "Enemy")
        {
            hasTouched = true;
            if (collision.gameObject.transform.parent == null)
                Debug.Log("Null parent");
            collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>().curHealth -= damage;
        }
        audioSource.Play();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }

}
