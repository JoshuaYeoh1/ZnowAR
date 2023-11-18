using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransform3D : MonoBehaviour
{
    [Header("Translate")]
    public bool randomTranslateX=false;
    public bool randomTranslateY=false, randomTranslateZ=false;
    public float minTranslate=-.1f, maxTranslate=.1f; 

    [Header("Rotate")]
    public bool randomRotateX=false;
    public bool randomRotateY=false, randomRotateZ=false;
    public float minRotate=-180, maxRotate=180;

    [Header("Scale")]
    public bool randomScaleX=true;
    public bool randomScaleY=true, randomScaleZ=true;
    public float minScale=.9f, maxScale=1.1f;
    
    // [Header("Mirror")]
    // public bool randomMirrorX=false;
    // public bool randomMirrorY=false, randomMirrorZ=false;

    void Awake()
    {
        translate();
        rotate();
        scale();
        //mirror();
    }

    void translate()
    {
        if(randomTranslateX)
            transform.localPosition = new Vector3(transform.localPosition.x+Random.Range(minTranslate,maxTranslate), transform.localPosition.y, transform.localPosition.z);

        if(randomTranslateY)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+Random.Range(minTranslate,maxTranslate), transform.localPosition.z);
            
        if(randomTranslateZ)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z+Random.Range(minTranslate,maxTranslate));
    }

    void rotate()
    {
        if(randomRotateX)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x+Random.Range(minRotate,maxRotate), transform.localEulerAngles.y, transform.localEulerAngles.z);
            
        if(randomRotateY)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y+Random.Range(minRotate,maxRotate), transform.localEulerAngles.z);
            
        if(randomRotateZ)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z+Random.Range(minRotate,maxRotate));
    }
    
    void scale()
    {  
        float uniformScale = Random.Range(minScale,maxScale);

        if(randomScaleX)
            transform.localScale = new Vector3(transform.localScale.x*uniformScale, transform.localScale.y, transform.localScale.z);

        if(randomScaleY)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y*uniformScale, transform.localScale.z);
            
        if(randomScaleZ)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z*uniformScale);
    }
    
    // colliders dont support negative scale
    
    // void mirror()
    // {
    //     if(randomMirrorX && Random.Range(1,3)==1)
    //         transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);

    //     if(randomMirrorY && Random.Range(1,3)==1)
    //         transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y*-1, transform.localScale.z);
            
    //     if(randomMirrorZ && Random.Range(1,3)==1)
    //         transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z*-1);
    // }
}
