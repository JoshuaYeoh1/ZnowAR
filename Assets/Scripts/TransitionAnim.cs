using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnim : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void playSfxSnow()
    {
        Singleton.instance.playSFX(Singleton.instance.sfxSnowTransition,transform,false);
    }
}
