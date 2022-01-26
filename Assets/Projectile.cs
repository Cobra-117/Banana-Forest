using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 1;

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<Enemy>().life -= damage;
        Destroy(this.gameObject);
    }

}
