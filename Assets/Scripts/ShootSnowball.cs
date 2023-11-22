using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSnowball : MonoBehaviour
{
    SceneScript scene;
    public GameObject snowballPrefab;

    public int snowballAmmo=10;
    public float shootForceMin=500, shootForceMax=1000, fireRate=.3f;
    public LayerMask pickupLayer;
    bool canShoot=true;

    void Awake()
    {
        scene=GameObject.FindGameObjectWithTag("Scene").GetComponent<SceneScript>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, 99999, pickupLayer, QueryTriggerInteraction.Collide))
            {
                if(hit.collider.tag=="Snowball")
                {
                    hit.collider.GetComponent<SnowballPickup>().pickupItem();
                }
                else shootSnowball(ray);
            }
            else shootSnowball(ray);
        }
    }

    void shootSnowball(Ray ray)
    {
        if(canShoot && snowballAmmo>0 && scene.gameStart)
        {
            Singleton.instance.playSFX(Singleton.instance.sfxSnowballShoot, transform);

            snowballAmmo--;

            StartCoroutine(shootCooldown());

            GameObject ball = Instantiate(snowballPrefab, ray.origin, Quaternion.identity);

            ball.GetComponent<Rigidbody>().AddForce(ray.direction * Random.Range(shootForceMin,shootForceMax));

            LeanTween.scale(ball, Vector3.zero, .5f).setDelay(5).setEaseInOutSine();

            Destroy(ball, 5.5f);
        }
    }

    IEnumerator shootCooldown()
    {
        canShoot=false;
        yield return new WaitForSeconds(fireRate);
        canShoot=true;
    }
}
