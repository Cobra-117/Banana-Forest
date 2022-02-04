using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoconutBomb : MonoBehaviour
{
    public float speedMultiplier;
    public int damage;
    public float range;

    public Vector3 StartPoint;
    public GameObject TargetObj;

    private float AnimationT;

    // Start is called before the first frame update
    void Start()
    {
        StartPoint = transform.position;
        AnimationT = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationT += Time.deltaTime;
        //AnimationT = AnimationT % 5;

        if (1 == 1)
        {
            //transform.Translate(new Vector3(-speed * Time.deltaTime, 0, -speed * Time.deltaTime));
            transform.position = Parabola(StartPoint, TargetObj.transform.position, 2f, AnimationT * speedMultiplier);
            if (DistToObj(TargetObj) < 0.1f)
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y) , range);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        explode();
    }

    /*From Ditzel Maths parabola class https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08 */
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

}