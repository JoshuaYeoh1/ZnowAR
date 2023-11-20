using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISnowballAmmo : MonoBehaviour
{
    public ShootSnowball snowball;
    InOutAnim anim;
    public TextMeshProUGUI tmpAmmo;
    bool canShow=true, canHide;

    void Awake()
    {
        anim = GetComponent<InOutAnim>();
    }

    void Update()
    {
        tmpAmmo.text = snowball.snowballAmmo.ToString();

        if(snowball.snowballAmmo>0 && canShow)
        {
            canShow=false;

            anim.animIn(.5f);

            Invoke("toggleHide",.5f);
        }
        else if(snowball.snowballAmmo<=0 && canHide)
        {
            canHide=false;

            anim.animOut(.5f);

            Invoke("toggleShow",.5f);
        }
    }

    void toggleHide()
    {
        canHide=!canHide;
    }

    void toggleShow()
    {
        canShow=!canShow;
    }
}
