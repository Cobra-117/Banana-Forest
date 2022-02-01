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
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            if (DistToTarget(Target) < 0.1f)
                explode();
        }
    }

    public void rotationToTarget(Vector3 _target)
    {
        Vector3 v_diff = (_target - transform.position);
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg + 180);
    }

    void explode()
    {
        Debug.Log("Boom");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y) , 5f);
        Debug.Log("size: " + colliders.Length.ToString());
        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log("I");
            if (colliders[i].gameObject.tag == "Enemy")
            {
                colliders[i].gameObject.GetComponent<Enemy>().curHealth -= 1;
                Debug.Log("Touched");
            }
        }
        Destroy(this.gameObject);
    }

    public float DistToTarget(Vector3 _Target)
    {
        float dist = 0;
        float dist_x = 0;
        float dist_y = 0;

        dist_x = transform.position.x - _Target.x;
        dist_y = transform.position.y - _Target.y;
        if (dist_x < 0)
            dist_x = dist_x * (-1);
        if (dist_y < 0)
            dist_y = dist_y * (-1);
        dist = (dist_x + dist_y);
        return (dist);
    }
}
