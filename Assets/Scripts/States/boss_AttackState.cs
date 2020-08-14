using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_AttackState : StateMachineBehaviour
{
    Boss boss;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.transform.Find("melee").GetComponent<BoxCollider2D>().enabled = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.nAttackCount = 1;
        Debug.Log("onStateExit Attack");
        boss.transform.Find("melee").GetComponent<BoxCollider2D>().enabled = false;
    }

}
