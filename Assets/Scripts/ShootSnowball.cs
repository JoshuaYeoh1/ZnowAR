using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSnowball : MonoBehaviour
{
    Camera cam;
    public GameObject snowballPrefab;
    public float shootForce=1000, fireRate=.3f;
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

            if(canShoot)
            {
                StartCoroutine(shootCooldown());

                GameObject ball = Instantiate(snowballPrefab, ray.origin, Quaternion.identity);

                ball.GetComponentInChildren<SphereCollider>().enabled=true;
            
                Rigidbody ballRb = ball.GetComponent<Rigidbody>();

                ballRb.isKinematic=false;

                ballRb.AddForce(ray.direction * shootForce);

                LeanTween.scale(ball, Vector3.zero, .5f).setDelay(5).setEaseInOutSine();

                Destroy(ball, 5.5f);
            }
        }
    }

    IEnumerator shootCooldown()
    {
        canShoot=false;
        yield return new WaitForSeconds(fireRate);
        canShoot=true;
    }
}
