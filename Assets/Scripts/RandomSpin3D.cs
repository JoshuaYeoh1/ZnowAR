using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpin3D : MonoBehaviour
{
    public float spinXSpeedMin=-1, spinXSpeedMax=1, spinYSpeedMin=-1, spinYSpeedMax=1, spinZSpeedMin=-1, spinZSpeedMax=1, spinSpeedMult=100;
    Vector3 spinSpeed;

    void Awake()
    {
        spinSpeed = new Vector3(Random.Range(spinXSpeedMin,spinXSpeedMax), Random.Range(spinYSpeedMin,spinYSpeedMax), Random.Range(spinZSpeedMin,spinZSpeedMax));
    }

    void Update()
    {
        transform.eulerAngles += spinSpeed*spinSpeedMult*Time.deltaTime;
    }
}
