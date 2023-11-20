using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballSpawner : MonoBehaviour
{
    SceneScript scene;
    public GameObject snowballPickup;

    void Start()
    {
        scene=GameObject.FindGameObjectWithTag("Scene").GetComponent<SceneScript>();
        scene.snowballSpawnerList.Add(gameObject);
    }

    public void spawnSnowball()
    {
        scene.snowballCount++;

        Instantiate(snowballPickup, new Vector3(transform.position.x, transform.position.y+.1f, transform.position.z), Quaternion.identity);
    }

    void OnDestroy()
    {
        scene.snowballSpawnerList.Remove(gameObject);
    }
}
