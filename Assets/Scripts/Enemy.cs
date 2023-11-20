using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SceneScript scene;
    Camera cam;
    Vector3 targetPos;
    Rigidbody rb;
    public WiggleMove shake;
    public GameObject skinsGroup;

    public float moveSpeedMin=1, moveSpeedMax=4;
    public float lookSpeed=5;
    public float stayTimeMin=3, stayTimeMax=5;
    public float rangeMin=1.25f, rangeMax=1.5f;
    public float wanderRange=5;
    float range, moveSpeed;
    public bool chase, inRange;

    public bool isMoving;
    public int atkAnim;

    HPManager hp;
    bool iframe;
    public float iframeTime=.5f;

    public GameObject atkHitbox;
    public float atkTimeMin=1, atkTimeMax=2;

    void Awake()
    {
        scene=GameObject.FindGameObjectWithTag("Scene").GetComponent<SceneScript>();
        cam=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        hp=GetComponent<HPManager>();
        rb=GetComponent<Rigidbody>();

        atkHitbox.SetActive(false);

        spawnAnim();
        movingRt=StartCoroutine(moving());
        atkingRt=StartCoroutine(attacking());
    }

    void spawnAnim()
    {
        Vector3 defScale = transform.localScale;
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, defScale, 1).setEaseOutBack();
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
        {
            isMoving=true;
            transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving=false;
        }
    }

    Coroutine movingRt, atkingRt;

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

    IEnumerator attacking()
    {
        while(true)
        {
            yield return new WaitForSeconds(rand(atkTimeMin,atkTimeMax));

            if(inRange && !isMoving)
            {
                atkAnim=Random.Range(0,2);

                foreach(Animator anim in skinsGroup.GetComponentsInChildren<Animator>())
                {
                    anim.SetTrigger("atk");
                }
            }
        }
    }

    public void attack()
    {
        StartCoroutine(doHitbox());
    }

    IEnumerator doHitbox()
    {
        atkHitbox.SetActive(true);
        yield return new WaitForSeconds(.1f);
        atkHitbox.SetActive(false);
    }

    public void hit(float dmg)
    {
        if(!iframe)
        {
            StartCoroutine(iframing());

            shake.shake(.1f);

            foreach(Animator anim in skinsGroup.GetComponentsInChildren<Animator>())
            {
                anim.SetTrigger("hit");
            }

            StartCoroutine(flashRed());
            
            hp.hit(dmg);

            if(hp.hp<=0) die();
        }
    }

    IEnumerator flashRed()
    {
        foreach(SkinnedMeshRenderer mesh in skinsGroup.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            List<Color> defcolors =  new List<Color>();

            for(int i=0; i<mesh.materials.Length; i++)
            {
                defcolors.Add(mesh.materials[i].color);

                mesh.materials[i].color = new Color(mesh.materials[i].color.r+.5f, mesh.materials[i].color.g-.5f, mesh.materials[i].color.b-.5f);
            }

            yield return new WaitForSeconds(.1f);

            for(int i=0; i<mesh.materials.Length; i++)
            {
                mesh.materials[i].color = new Color(defcolors[i].r, defcolors[i].g, defcolors[i].b);
            }
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
        if(movingRt!=null) StopCoroutine(movingRt);
        if(atkingRt!=null) StopCoroutine(atkingRt);

        iframe=true;

        rb.useGravity=true;
        rb.isKinematic=false;

        foreach(Animator anim in skinsGroup.GetComponentsInChildren<Animator>())
        {
            anim.SetTrigger("die");
        }

        scene.playerKills++;
    }

    public void disappear()
    {
        StartCoroutine(disappearing());
    }

    IEnumerator disappearing()
    {
        yield return new WaitForSeconds(1);

        LeanTween.scale(gameObject, Vector3.zero, .5f).setEaseOutBack();

        yield return new WaitForSeconds(.5f);

        scene.enemyList.Remove(gameObject);

        Destroy(gameObject);
    }

    // public GameObject testSnowball;

    // void LateUpdate()
    // {
    //     if(Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Instantiate(testSnowball, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), Quaternion.identity);
    //     }
    // }
}
