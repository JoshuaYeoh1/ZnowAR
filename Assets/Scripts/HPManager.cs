using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public bool regen;
    public float hp, hpMax, regenHp=1, regenTime=1;
    public GameObject hpBarFill;
    public InOutAnim hpBar;
    bool canShow=true, canHide;

    void Awake()
    {
        hp=hpMax;
        StartCoroutine(hpregen());
    } 

    void Update()
    {
        if(hp>hpMax) hp=hpMax;
        else if(hp<0) hp=0;

        if(hp>0 && hp<hpMax && canShow)
        {
            toggleShow();

            hpBar.animIn(.5f);

            Invoke("toggleHide",.5f);
        }
        else if((hp<=0 || hp>=hpMax) && canHide)
        {
            toggleHide();

            hpBar.animOut(.5f);

            Invoke("toggleShow",.5f);
        }
    }

    void toggleHide()
    {
        canHide=!canHide;
    }

    void toggleShow()
    {
        canShow=!canShow;
    }

    public void hit(float dmg)
    {
        if(hp>dmg) hp-=dmg;
        else hp=0;
        updateHpBar();
    }    
    
    IEnumerator hpregen()
    {
        while(true)
        {
            yield return new WaitForSeconds(regenTime);

            if(regen && hp>0 && hp<hpMax)
            {   
                if(hp<=hpMax-regenHp) hp+=regenHp;
                else hp=hpMax;
                updateHpBar();
            }
        }
    }

    int hpBarLt=0;

    public void updateHpBar()
    {
        if(hpBarFill)
        {
            LeanTween.cancel(hpBarLt);
            hpBarLt = LeanTween.scaleX(hpBarFill, hp/hpMax, .2f).setEaseOutSine().id;
        }
    }
}
