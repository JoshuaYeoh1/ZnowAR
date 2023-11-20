using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSnowball : MonoBehaviour
{
    Camera cam;
    public GameObject snowballPrefab;

    public int snowballAmmo=10;
    public float shootForceMin=500, shootForceMax=1000, fireRate=.3f;
    public LayerMask pickupLayer;
    bool canShoot=true;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

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
        if(canShoot && snowballAmmo>0)
        {
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
