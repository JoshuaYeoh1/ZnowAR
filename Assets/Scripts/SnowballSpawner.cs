using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballSpawner : MonoBehaviour
{
    public GameObject snowballPickup;

    void Start()
    {
        Singleton.instance.snowballSpawnerList.Add(gameObject);
    }

    public void spawnSnowball()
    {
        Singleton.instance.snowballCount++;

        Instantiate(snowballPickup, transform.position, Quaternion.identity);
    }

    void OnDestroy()
    {
        Singleton.instance.snowballSpawnerList.Remove(gameObject);
    }
}
