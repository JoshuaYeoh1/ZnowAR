using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public float dmg=10;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            other.GetComponent<Player>().hit(dmg);

            Singleton.instance.playSFX(Singleton.instance.sfxEnemyPunch, other.transform);
        }
    }
}
