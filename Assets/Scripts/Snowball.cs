using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public GameObject particles;
    public int bouncecountMin=1, bouncecountMax=3;
    int bouncecount;
    public float dmgMin=1, dmgMax=2;

    void Awake()
    {
        bouncecount=Random.Range(bouncecountMin,bouncecountMax);
    }

    void OnCollisionEnter(Collision other)
    {
        bouncecount--;

        if(other.gameObject.tag=="Enemy")
        {
            other.gameObject.GetComponent<EnemyCollider>().enemy.hit(Random.Range(dmgMin,dmgMax));
        }

        if(bouncecount<=0)
        {
            Instantiate(particles, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
