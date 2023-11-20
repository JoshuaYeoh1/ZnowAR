using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public Enemy enemy;
    Animator anim;

    void Awake()
    {
        anim=GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("moving", enemy.isMoving);
        anim.SetInteger("atkAnim", enemy.atkAnim);
    }

    public void atk()
    {
        enemy.attack();
    }

    public void disappear()
    {
        enemy.disappear();
    }
}
