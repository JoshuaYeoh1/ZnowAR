using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleRotate : MonoBehaviour
{
    public float frequency, magnitude;
    float angleX, angleY;
    public bool wiggle;
    float seed;

    void Start()
    {
        seed = Random.value;
    }

    void LateUpdate()
    {
        if(wiggle)
        {
            angleX = Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1;
            angleY = Mathf.PerlinNoise(seed+1, Time.time * frequency) * 2 - 1;

            transform.localEulerAngles = new Vector3(angleX,angleY,0) * magnitude;
        }
    }

    Coroutine rt1;

    public void shake(float time)
    {
        if(rt1!=null)
        StopCoroutine(rt1);
        
        rt1 = StartCoroutine(shaker(time));
    }

    IEnumerator shaker(float time)
    {
        transform.localEulerAngles = Vector3.zero;

        wiggle=true;

        yield return new WaitForSeconds(time);

        wiggle=false;

        transform.localEulerAngles = Vector3.zero;
    }
}
