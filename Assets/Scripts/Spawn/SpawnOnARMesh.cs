using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnOnARMesh : MonoBehaviour
{
    GameObject player;
    MeshAnalyser meshAnalyser;
    Mesh arMesh; 

    public float minVertsForSpawn, minRangeFromPlayer=4;

    public List<GameObject> spawnObjects = new List<GameObject>();
    public int spawnLikelyHood = 33;
    public bool ignoreGround;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(spawnLikelyHood == 0) return;
        meshAnalyser = GetComponent<MeshAnalyser>();
        meshAnalyser.analysisDone += StartSpawning;
    }

    void StartSpawning()
    {
        arMesh = GetComponent<MeshFilter>().sharedMesh;

        int spawnLikely =  Random.Range(0, 100 / spawnLikelyHood);

        if (spawnLikely != 0) return;

        if (arMesh.vertexCount > minVertsForSpawn)
        {
            if(ignoreGround || meshAnalyser.IsGround)
            {
                if(Vector3.Distance(player.transform.position, transform.position)>minRangeFromPlayer)
                {
                    InstantiateObject(GetRandomObject());
                }
            }
        }
    }

    GameObject GetRandomObject()
    {
        return spawnObjects[Random.Range(0, spawnObjects.Count)];
    }

    void InstantiateObject(GameObject obj)
    {
        GameObject spawned = Instantiate(obj, GetRandomVector(), Quaternion.identity);

        Vector3 defScale = spawned.transform.localScale;

        spawned.transform.localScale = Vector3.zero;

        LeanTween.scale(spawned, defScale, Random.Range(.5f, 1)).setEaseOutBack();
    }

    Vector3 GetRandomVector()
    {
        Vector3 highestVert = Vector3.zero;
        float highestY = Mathf.NegativeInfinity;

        foreach (var vert in arMesh.vertices)
        {
            if (vert.y > highestY)
            {
                highestY = vert.y;
                highestVert = transform.TransformPoint(vert);
            }
        }
        
        return highestVert;
    }

    void OnDestroy()
    {
        meshAnalyser.analysisDone -= StartSpawning;
    }
}
