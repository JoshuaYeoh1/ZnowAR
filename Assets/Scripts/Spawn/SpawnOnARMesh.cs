using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnOnARMesh : MonoBehaviour
{
    MeshAnalyser meshAnalyser;
    Mesh arMesh; 
    public List<GameObject> spawnObjects = new List<GameObject>();
    
    public float minVertsForSpawn;
    public int spawnLikelyHood = 33;
    public bool ignoreGround;

    void Start()
    {
        if(spawnLikelyHood == 0) return;

        meshAnalyser = GetComponent<MeshAnalyser>();
        meshAnalyser.analysisDone += StartSpawning;
    }

    void StartSpawning()
    {
        arMesh = GetComponent<MeshFilter>().sharedMesh;

        int spawnLikely =  Random.Range(0, 100 / spawnLikelyHood);
        Debug.Log("Spawnlikely => " + spawnLikely);

        if (spawnLikely != 0) return;

        if (arMesh.vertexCount > minVertsForSpawn)
        {
            if(ignoreGround || meshAnalyser.IsGround)
            InstantiateObject(GetRandomObject());
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

        Singleton.instance.playSFX(Singleton.instance.sfxPropSpawn, spawned.transform);
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
