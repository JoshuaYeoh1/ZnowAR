using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISnowballAmmo : MonoBehaviour
{
    InOutAnim anim;
    public TextMeshProUGUI tmpAmmo;
    bool canShow=true, canHide;

    void Awake()
    {
        anim = GetComponent<InOutAnim>();
    }

    void Update()
    {
        tmpAmmo.text = Singleton.instance.snowballAmmo.ToString();

        if(Singleton.instance.snowballAmmo>0 && canShow)
        {
            canShow=false;

            anim.animIn(.5f);

            Invoke("toggleHide",.5f);
        }
        else if(Singleton.instance.snowballAmmo<=0 && canHide)
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
