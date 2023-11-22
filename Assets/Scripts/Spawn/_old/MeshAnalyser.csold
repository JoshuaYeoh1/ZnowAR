using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAnalyser : MonoBehaviour
{
    MeshFilter _meshFilter;
    MeshRenderer mr;

    public float groundThreshold;
    public float avgNormal;
    public float minVerts;
    public bool isGround;

    public Material groundMaterial;

    public bool IsGround
    {
        get => isGround; 
    }

    public float AvgNorm
    {
        get => avgNormal;
    }

    public event Action analysisDone; 

    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        mr = GetComponent<MeshRenderer>();
        
        if(_meshFilter != null)
         StartCoroutine(CheckForGround());
    }

    IEnumerator CheckForGround()
    {
        yield return new WaitUntil(() =>
        {
            return _meshFilter.sharedMesh.vertices.Length > minVerts;
        });

        isGround = AnalyseForGround(_meshFilter.sharedMesh);
        analysisDone?.Invoke();
    }
    
    bool AnalyseForGround(Mesh mesh)
    {
        float averageVert = 0;
        
        foreach (var normal in mesh.normals)
        {
            averageVert += normal.normalized.y;
        }

        averageVert /= mesh.vertices.Length;
        avgNormal =  averageVert;
        // mat.SetFloat("_LerpValue", avgNormal);

        if (averageVert >= groundThreshold)
        {
            return true;
        }

        return false;
    }

    void Update()
    {
        if(isGround && mr.material != groundMaterial)
        {
            mr.material = groundMaterial;
        }
    }
}
