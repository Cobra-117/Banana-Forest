using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutBomb : MonoBehaviour
{
    public float speed = 0;
    public int damage = 1;

    public Vector3 Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    }

    public void rotationToTarget(Vector3 _target)
    {
        Vector3 v_diff = (_target - transform.position);
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg + 180);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<Enemy>().curHealth -= damage;
        Destroy(this.gameObject);
    }
}
