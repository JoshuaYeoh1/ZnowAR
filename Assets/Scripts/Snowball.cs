using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public GameObject particles;
    public int bouncecount=3;

    void OnCollisionEnter(Collision other)
    {
        bouncecount--;

        if(bouncecount<=0)
        {
            Instantiate(particles, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
