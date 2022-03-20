using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acidRain : MonoBehaviour
{
    public bool isRaining;
    public float Countdown;
    public GameObject ParticleSystem;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void ActivateRain()
    {
        isRaining = true;
        Countdown = 7;
        ParticleSystem.SetActive(true);
        audioSource.Play();
    }

    void StopRain()
    {
        isRaining = false;
        audioSource.Stop();
        ParticleSystem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaining == true)
        {
            Countdown -= Time.deltaTime;
            if (Countdown <= 0)
            {
                StopRain();
            }
            poisonEnnemies();
        }
    }

    void poisonEnnemies()
    {
        GameObject[] ennemies = null;
        Enemy script;

        ennemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject ennemy in ennemies)
        {
            Debug.Log("Poisonning enemy");
            if (ennemy.transform.parent.gameObject.GetComponent<Enemy>() == null)
                continue;
            //ennemy.GetComponent<Enemy>();
            ennemy.transform.parent.gameObject.GetComponent<Enemy>().setPoison(0.1f, 0.9f);
            //ennemy.GetComponent<Enemy>().isPoisoned = true;
            //ennemy.GetComponent<Enemy>().PoisonPower = 0.7f;
            //ennemy.GetComponent<Enemy>().poisonCoutdown = 0.1f;
        }
    }
}
