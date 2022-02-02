using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaProjectile : MonoBehaviour
{
    public GameObject Target;
    public float speed = 0;
    public float RotationSpeed;

    private Vector3 startPos;
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        lookAtEnemy(Target);
        child = this.gameObject.transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            child.transform.localEulerAngles = new Vector3(child.transform.localEulerAngles.x, child.transform.localEulerAngles.y,
            child.transform.localEulerAngles.z + RotationSpeed * Time.deltaTime);
        }
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

    public void lookAtEnemy(GameObject Enemy)
    {
        Debug.Log("looked at");
        Vector3 v_diff = (Enemy.transform.position - transform.position);
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg + 180);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<Enemy>().curHealth -= 1;
    }
}
