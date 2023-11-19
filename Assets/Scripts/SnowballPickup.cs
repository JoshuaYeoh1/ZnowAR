using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballPickup : MonoBehaviour
{
    Camera cam;
    bool canPickup;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        StartCoroutine(spawnAnim(.5f));
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, cam.transform.position)>5 && canPickup)
        {
            destroyPickup();
        }
    }

    IEnumerator spawnAnim(float time)
    {
        Vector3 defScale = transform.localScale;

        transform.localScale = Vector3.zero;

        LeanTween.scale(gameObject, defScale, time).setEaseOutBack();

        yield return new WaitForSeconds(time);

        canPickup=true;
    }

    public void pickupItem()
    {
        if(canPickup)
        {
            canPickup=false;

            StartCoroutine(pickingUp());
        }
    }

    IEnumerator pickingUp()
    {
        Vector3 defScale = transform.localScale;
        LeanTween.scale(gameObject, defScale*1.1f, .1f).setEaseInOutSine();
        yield return new WaitForSeconds(.1f);
        LeanTween.scale(gameObject, defScale, .1f).setEaseInOutSine();

        yield return new WaitForSeconds(.1f);
        LeanTween.move(gameObject, cam.transform.position, .25f).setEaseInOutSine();

        yield return new WaitForSeconds(.25f);
        Singleton.instance.snowballAmmo++;

        destroyPickup();
    }

    void destroyPickup()
    {
        if(Singleton.instance.snowballCount>0) Singleton.instance.snowballCount--;

        LeanTween.scale(gameObject, Vector3.zero, .5f).setEaseInBack();

        Destroy(gameObject, .5f);
    }
}
