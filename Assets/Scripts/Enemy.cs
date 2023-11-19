using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Camera cam;
    Vector3 targetPos;
    public WiggleMove shake;

    public float moveSpeedMin=1, moveSpeedMax=4;
    public float lookSpeed=5;
    public float stayTimeMin=3, stayTimeMax=5;
    public float rangeMin=.7f, rangeMax=1;
    public float wanderRange=5;
    float range, moveSpeed;
    public bool chase, inRange;
    bool iframe=true;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        StartCoroutine(spawnAnim(.5f));

        StartCoroutine(moving());
    }

    IEnumerator spawnAnim(float time)
    {
        Vector3 defScale = transform.localScale;

        transform.localScale = Vector3.zero;

        LeanTween.scale(gameObject, defScale, time).setEaseOutBack();

        yield return new WaitForSeconds(time);

        iframe=false;
    }

    void Update()
    {
        moveToTarget(targetPos);

        if(Vector3.Distance(transform.position,targetPos)<=rangeMax && chase) inRange=true;
        else inRange=false;
    }

    void moveToTarget(Vector3 pos)
    {
        Vector3 lookDirection = pos - transform.position;
        lookDirection.Normalize();

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), lookSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position,targetPos)>range)
        transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
    }

    IEnumerator moving()
    {
        while(true)
        {
            range = rand(rangeMin, rangeMax);
            moveSpeed = rand(moveSpeedMin, moveSpeedMax);

            if(chase)
            {
                targetPos = cam.transform.position;

                yield return new WaitForSeconds(rand(stayTimeMin,stayTimeMax));
            }
            else
            {
                randTargetPos(-wanderRange,wanderRange);

                yield return new WaitForSeconds(rand(stayTimeMin*.5f,stayTimeMax*.5f));
            }

            if(Random.Range(1,3)==1) chase=!chase;
        }
    }

    void randTargetPos(float min, float max)
    {
        targetPos = new Vector3(cam.transform.position.x+rand(min,max), cam.transform.position.y+rand(0,max), cam.transform.position.z+rand(min,max));
    }

    float rand(float min=-1, float max=1)
    {
        return Random.Range(min, max);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag=="PlayerBullet")
        {
            hit(1);
        }
    }

    public void hit(float dmg)
    {
        if(!iframe)
        {
            shake.shake(.1f);

            //play hurt anim

            //flash red
        }
    }

    IEnumerator die()
    {
        iframe=true;

        shake.shake(.1f);

        //play death anim

        yield return new WaitForSeconds(.1f); // wait death anim

        LeanTween.scale(gameObject, Vector3.zero, .5f).setEaseOutBack();

        yield return new WaitForSeconds(.5f);

        Singleton.instance.enemyList.Remove(gameObject);

        Destroy(gameObject);
    }
}
