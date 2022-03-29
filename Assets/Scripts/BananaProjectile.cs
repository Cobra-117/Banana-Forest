using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaProjectile : MonoBehaviour
{
    public GameObject Target;
    public float speed = 0;
    public float RotationSpeed;
    public float range;
    public GameObject child;
    public float minSpeed;
    public int damage;

    private Vector3 startPos;
    private bool goBack = false;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        lookAtEnemy(Target);
        child = this.gameObject.transform.GetChild(0).gameObject;
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float realSpeed;
        float realRotSpeed;

        if (speed != 0)
        {
            if (goBack == false)
            {
                realSpeed = speed * ((range - DistToTarget(startPos))) / range;
                if (realSpeed < minSpeed)
                    realSpeed = minSpeed;
                realRotSpeed = RotationSpeed * ((range - DistToTarget(startPos))) / range * 2;
                if (realRotSpeed < 200)
                    realRotSpeed = 200;
                transform.Translate(new Vector3(-realSpeed * Time.deltaTime, 0, 0));
                child.transform.localEulerAngles = new Vector3(child.transform.localEulerAngles.x, child.transform.localEulerAngles.y,
                child.transform.localEulerAngles.z + realRotSpeed * Time.deltaTime);
                if (DistToTarget(startPos) >= range)
                {
                    goBack = true;
                }
            }
            else
            {
                realSpeed = speed * ((range - DistToTarget(startPos))) / range;
                if (realSpeed < minSpeed)
                    realSpeed = minSpeed;
                realRotSpeed = RotationSpeed * ((range - DistToTarget(startPos))) / range * 2;
                if (realRotSpeed < 200)
                    realRotSpeed = 200;
                transform.Translate(new Vector3(realSpeed * Time.deltaTime, 0, 0));
                child.transform.localEulerAngles = new Vector3(child.transform.localEulerAngles.x, child.transform.localEulerAngles.y,
                child.transform.localEulerAngles.z - realRotSpeed * Time.deltaTime);
                if (DistToTarget(startPos) <= 0.1f)
                    Destroy(this.gameObject);
            }
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

    public float DistToTarget(Vector3 _target)
    {
        float dist = 0;
        float dist_x = 0;
        float dist_y = 0;

        dist_x = transform.position.x - _target.x;
        dist_y = transform.position.y - _target.y;
        if (dist_x < 0)
            dist_x = dist_x * (-1);
        if (dist_y < 0)
            dist_y = dist_y * (-1);
        dist = (dist_x + dist_y);
        return (dist);
    }


    public void lookAtEnemy(GameObject Enemy)
    {
        Vector3 v_diff = (Enemy.transform.position - transform.position);
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg + 180);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>().curHealth -= damage;
            audioSource.Play();
        }
    }
}
