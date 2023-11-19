using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRandomizer : MonoBehaviour
{
    public List<GameObject> skinList = new List<GameObject>();
    GameObject chosenSkin;

    void Awake()
    {
        chosenSkin = skinList[Random.Range(0,skinList.Count)];

        for(int i=skinList.Count-1; i>=0; i--)
        {
            if(skinList[i]!=chosenSkin)
            {
                Destroy(skinList[i]);
                skinList.Remove(skinList[i]);
            }
            else skinList[i].SetActive(true);
        }
    }
}
