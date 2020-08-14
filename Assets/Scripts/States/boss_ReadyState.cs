using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_ReadyState : StateMachineBehaviour
{
    Boss boss;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(boss.transform.position,boss.player.position) <= 2.5f && boss.attackDelay <= float.Epsilon)
        {
            boss.attackDelay = boss.attackCoolDown;
            animator.SetTrigger("Attack");
        }

        if(Vector2.Distance(boss.transform.position,boss.player.position) > 3.51f)
        {
            animator.SetBool("isFollow", true);
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
