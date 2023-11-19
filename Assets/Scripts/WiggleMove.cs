using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleMove : MonoBehaviour
{
    public float frequency=1, magnitude=.1f;
    public bool wiggle=true, doX=true, doY=true, doZ=true;
    float seed, x, y, z;

    void Start()
    {
        seed = Random.value*9999;
    }

    void LateUpdate()
    {
        if(wiggle)
        {
            if(doX) x = Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1;
            if(doY) y = Mathf.PerlinNoise(seed+1, Time.time * frequency) * 2 - 1;
            if(doZ) z = Mathf.PerlinNoise(seed+2, Time.time * frequency) * 2 - 1;

            transform.localPosition = new Vector3(x,y,z) * magnitude;
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
