using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InOutAnim : MonoBehaviour
{
    RectTransform rt;

    [Header("Position")]
    public bool animatePosition;
    public Vector2 startPosition, endPosition;
    Vector2 defaultPosition;

    [Header("Rotation")]
    public bool animateRotation;
    public float startRotation, endRotation;
    float defaultRotation;

    [Header("Scale")]
    public bool animateScale;
    public Vector2 startScale, endScale;
    Vector2 defaultScale;

    [Header("Alpha")]
    public bool animateAlpha;
    public float startAlpha, inAlpha=1, outAlpha;

    void Awake()
    {
        rt=GetComponent<RectTransform>();

        defaultPosition = transform.localPosition;
        defaultRotation = transform.eulerAngles.z;
        defaultScale = transform.localScale;

        resetState();
    }

    void resetState()
    {
        if(animatePosition)
        {
            transform.localPosition = defaultPosition+startPosition;
        }
        if(animateRotation)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, startRotation);
        }
        if(animateScale)
        {
            transform.localScale = startScale;
        }
        if(animateAlpha)
        {            
            LeanTween.alpha(rt, startAlpha, 0);
        }
    }

    int lt1=0;

    public void animIn(float time)
    {
        LeanTween.cancel(lt1);

        resetState();

        if(animatePosition)
        {
            lt1 = LeanTween.moveLocal(gameObject, defaultPosition, time).setEaseOutExpo().id;
        }
        if(animateRotation)
        {
            lt1 = LeanTween.rotate(gameObject, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, defaultRotation), time).setEaseInOutSine().id;
        }
        if(animateScale)
        {
            lt1 = LeanTween.scale(gameObject, defaultScale, time).setEaseOutBack().id;
        }
        if(animateAlpha && rt!=null)
        {
            lt1 = LeanTween.alpha(rt, inAlpha, time).setEaseInOutSine().id;
        }
    }

    public void animOut(float time)
    {
        LeanTween.cancel(lt1);

        if(animatePosition)
        {
            lt1 = LeanTween.moveLocal(gameObject, defaultPosition+endPosition, time).setEaseInExpo().id;
        }
        if(animateRotation)
        {
            lt1 = LeanTween.rotate(gameObject, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, endRotation), time).setEaseInOutSine().id;
        }
        if(animateScale)
        {
            lt1 = LeanTween.scale(gameObject, endScale, time).setEaseInBack().id;
        }
        if(animateAlpha && rt)
        {
            lt1 = LeanTween.alpha(rt, outAlpha, time).setEaseInOutSine().id;
        }
        
        Invoke("resetState",time);
    }
}
