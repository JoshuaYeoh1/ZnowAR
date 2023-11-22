using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour
{
    Image img;
    SpriteRenderer sr;
    Vector2 defscale;

    public float animTime=.3f, scaleMult=.1f;

    void Awake()
    {
        img = GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
        defscale = transform.localScale;
    }

    public void OnMouseEnter()
    {        
        LeanTween.scale(gameObject, new Vector2(defscale.x*(1+scaleMult),defscale.y*(1+scaleMult)), animTime).setEaseOutExpo().setIgnoreTimeScale(true);

        //Singleton.instance.playSFX(Singleton.instance.sfxBtnHover,transform,false);
    }

    public void OnMouseExit()
    {
        LeanTween.scale(gameObject, defscale, animTime).setEaseOutExpo().setIgnoreTimeScale(true);
    }

    public void ResetButtons()
    {
        transform.localScale = defscale;
    }

    public void OnMouseDown()
    {
        LeanTween.scale(gameObject, new Vector2(defscale.x*(1-scaleMult),defscale.y*(1-scaleMult)), animTime/2).setEaseOutExpo().setIgnoreTimeScale(true);

        LeanTween.scale(gameObject, new Vector2(defscale.x*(1+scaleMult),defscale.y*(1+scaleMult)), animTime/2).setDelay(animTime/2).setEaseOutBack().setIgnoreTimeScale(true);

        Singleton.instance.playSFX(Singleton.instance.sfxUiClick,transform,false);
    }
}
