using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 1;

    private bool hasTouched = false;

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasTouched == true)
            return;
        hasTouched = true;
        Debug.Log("collision");
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.transform.parent == null)
                Debug.Log("Null parent");
            collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>().curHealth -= damage;
        }
        Destroy(this.gameObject);
    }

}
