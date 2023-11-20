using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    HPManager hp;
    public InOutAnim redScreen;
    public InOutAnim diedText;

    bool iframe;
    public float iframeTime=.5f;

    void Awake()
    {
        hp=GetComponent<HPManager>();
    }

    public void hit(float dmg)
    {
        if(!iframe)
        {
            StartCoroutine(iframing());

            hp.hit(dmg);

            if(hp.hp<=0) die();
            else StartCoroutine(hurtAnim());
        }
    }

    IEnumerator iframing()
    {
        iframe=true;
        yield return new WaitForSeconds(iframeTime);
        iframe=false;
    }

    void die()
    {
        iframe=true;
        gameObject.layer=0;
        StartCoroutine(dieAnim());
    }

    IEnumerator hurtAnim()
    {
        redScreen.animIn(.1f);
        yield return new WaitForSeconds(.1f);
        redScreen.animOut(.5f);
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator dieAnim()
    {
        redScreen.animIn(.1f);
        diedText.gameObject.SetActive(true);
        diedText.animIn(.5f);
        yield return new WaitForSeconds(1);
        Singleton.instance.ReloadScene();
    }
}
