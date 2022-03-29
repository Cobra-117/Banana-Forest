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
    public AudioSource audioSource;

    private float AnimationT;
    private Vector2 SavedTarget;
    private bool hasTouched = false;

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

        if (TargetObj != null)
        {
            transform.position = Parabola(StartPoint, TargetObj.transform.position, 2f, AnimationT * speedMultiplier);
            SavedTarget = new Vector3(TargetObj.transform.position.x, TargetObj.transform.position.y, TargetObj.transform.position.z);
            if (DistToObj(TargetObj) < 0.1f)
                explode();
        }
        else
        {
            transform.position = Parabola(StartPoint, SavedTarget, 2f, AnimationT * speedMultiplier);
            if (DistToTarget(SavedTarget) < 0.1f)
                explode();
        }
        if (hasTouched == true && !audioSource.isPlaying)
            Destroy(this.gameObject);
    }

    public void rotationToTarget(Vector3 _target)
    {
        Vector3 v_diff = (_target - transform.position);
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg + 180);
    }

    void explode()
    {
        if (hasTouched == true)
            return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y) , range);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy")
            {
                colliders[i].gameObject.transform.parent.gameObject.GetComponent<Enemy>().curHealth -= damage;
            }
        }
        hasTouched = true;
        audioSource.Play();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
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

    public float DistToTarget(Vector3 TargetPos)
    {
        float dist = 0;
        float dist_x = 0;
        float dist_y = 0;

        dist_x = transform.position.x - TargetPos.x;
        dist_y = transform.position.y - TargetPos.y;
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
