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

        Instantiate(snowballPickup, new Vector3(transform.position.x, transform.position.y+.1f, transform.position.z), Quaternion.identity);
    }

    void OnDestroy()
    {
        Singleton.instance.snowballSpawnerList.Remove(gameObject);
    }
}
