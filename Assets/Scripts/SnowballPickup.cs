using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballPickup : MonoBehaviour
{
    SceneScript scene;
    ShootSnowball player;
    bool canPickup;

    void Awake()
    {
        scene=GameObject.FindGameObjectWithTag("Scene").GetComponent<SceneScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootSnowball>();

        StartCoroutine(spawnAnim(.5f));
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)>5 && canPickup)
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
        LeanTween.move(gameObject, player.transform.position, .25f).setEaseInOutSine();

        yield return new WaitForSeconds(.25f);
        player.snowballAmmo++;

        destroyPickup();
    }

    void destroyPickup()
    {
        if(scene.snowballCount>0) scene.snowballCount--;

        LeanTween.scale(gameObject, Vector3.zero, .5f).setEaseInBack();

        Destroy(gameObject, .5f);
    }
}
