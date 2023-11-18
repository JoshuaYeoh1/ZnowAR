using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnOnARMesh : MonoBehaviour
{
    public MeshAnalyser meshAnalyser;
    Mesh arMesh; 

    public float minVertsForSpawn;

    public List<GameObject> spawnObjects = new List<GameObject>();
    public int spawnLikelyHood = 33; 

    void Start()
    {
        if(spawnLikelyHood == 0) return;
        //meshAnalyser = GetComponent<MeshAnalyser>();
        meshAnalyser.analysisDone += StartSpawning;
    }

    void StartSpawning()
    {
        arMesh = GetComponent<MeshFilter>().sharedMesh;

        int spawnLikely =  Random.Range(0, 100 / spawnLikelyHood);

        if (spawnLikely != 0) return;

        if (arMesh.vertexCount > minVertsForSpawn && meshAnalyser.IsGround) InstantiateObject(GetRandomObject());
    }

    GameObject GetRandomObject()
    {
        return spawnObjects[Random.Range(0, spawnObjects.Count)];
    }

    void InstantiateObject(GameObject obj)
    {
        GameObject spawned = Instantiate(obj, GetRandomVector(), Quaternion.identity);

        Vector3 oriScale = spawned.transform.localScale;

        spawned.transform.localScale = Vector3.zero;

        LeanTween.scale(spawned, oriScale, .5f).setEaseOutBack();
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
