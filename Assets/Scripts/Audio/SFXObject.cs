using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXObject : MonoBehaviour
{
    AudioSource source;
    //float camRadiusX;
    [HideInInspector] public bool randPitch=true, dynamics=true;

    void Awake()
    {
        source = GetComponent<AudioSource>();        
    }

    void Start()
    {
        if(randPitch) source.pitch += Random.Range(-.2f,.2f);
        else source.pitch = 1;

        if(dynamics) source.spatialBlend = 1;
        else source.spatialBlend = 0;
    }

    // void Update()
    // {
    //     if(dynamics)
    //     {
    //         camRadiusX = Camera.main.orthographicSize*16/9;

    //         source.panStereo = (transform.position.x-Camera.main.transform.position.x)/camRadiusX;
    //     }
    //     else
    //         source.panStereo = 0;
    // }
}
