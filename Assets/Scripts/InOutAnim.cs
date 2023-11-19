using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InOutAnim : MonoBehaviour
{
    RectTransform rt;
    Image img;
    // Image[] img2;
    // TextMeshProUGUI tmp;
    // TextMeshProUGUI[] tmp2;

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
    public float startAlpha, endAlpha;
    float defaultAlpha;

    void Awake()
    {
        rt=GetComponent<RectTransform>();
        img=GetComponent<Image>();
        // img2=GetComponentsInChildren<Image>();
        // tmp=GetComponent<TextMeshProUGUI>();
        // tmp2=GetComponentsInChildren<TextMeshProUGUI>();

        defaultPosition = transform.localPosition;
        defaultRotation = transform.eulerAngles.z;
        defaultScale = transform.localScale;

        if(img!=null)
            defaultAlpha = img.color.a;

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
        if(animateAlpha && img!=null)
        {            
            img.color = new Color(img.color.r, img.color.g, img.color.b, startAlpha);
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
            lt1 = LeanTween.alpha(rt, defaultAlpha, time).setEaseInOutSine().id;
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
        if(animateAlpha && rt!=null)
        {
            lt1 = LeanTween.alpha(rt, endAlpha, time).setEaseInOutSine().id;
        }
        
        Invoke("resetState",time);
    }

    // void Update()
    // {
    //     if(tmp!=null)
    //         tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, img.color.a);

    //     if(img2!=null)
    //     {
    //         for(int i=0;i<img2.Length;i++)
    //         {
    //             img2[i].color = new Color(img2[i].color.r, img2[i].color.g, img2[i].color.b, img.color.a);
    //         }
    //     }

    //     if(tmp2!=null)
    //     {
    //         for(int i=0;i<tmp2.Length;i++)
    //         {
    //             tmp2[i].color = new Color(tmp2[i].color.r, tmp2[i].color.g, tmp2[i].color.b, img.color.a);
    //         }
    //     }
    // }
}
