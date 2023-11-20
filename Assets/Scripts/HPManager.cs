using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public bool regen;
    public float hp, hpMax, regenHp=1, regenTime=1;
    public GameObject hpBar;

    void Awake()
    {
        hp=hpMax;

        StartCoroutine(hpregen());
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
        if(hpBar)
        {
            LeanTween.cancel(hpBarLt);
            hpBarLt = LeanTween.scaleX(hpBar, hp/hpMax, .2f).setEaseOutSine().id;
        }
    }

    void Update()
    {
        if(hp>hpMax) hp=hpMax;
        else if(hp<0) hp=0;
    }
}
