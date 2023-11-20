using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    public bool onlyY;

    void Update()
    {
        if(onlyY) transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        else transform.rotation = Camera.main.transform.rotation;
    }
}
