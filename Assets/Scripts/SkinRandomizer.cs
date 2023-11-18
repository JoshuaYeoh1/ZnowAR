using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRandomizer : MonoBehaviour
{
    public bool enable=true;
    public GameObject[] skins;
    int skin;

    void Awake()
    {
        if(enable)
        {
            skin = Random.Range(0,skins.Length);

            for(int i=0; i<skins.Length; i++)
            {
                if(i!=skin) skins[i].SetActive(false);
                else skins[i].SetActive(true);
            }

            //deleteUnused();
        }
    }

    void deleteUnused()
    {
        for(int i=0; i<skins.Length; i++)
        {
            if(i!=skin) Destroy(skins[i]);
        }
    }
}
