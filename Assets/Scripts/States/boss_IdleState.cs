using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_IdleState : StateMachineBehaviour
{
    Boss boss;
    Transform bossTransform;

    float setupTime = 5f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        bossTransform = animator.GetComponent<Boss>().transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(boss.player.position, bossTransform.position) <= 6)
            animator.SetBool("isFollow", true);

        setupTime -= Time.deltaTime;

        if(setupTime <= 0.1f)
        {
            animator.SetBool("isWander", true);
        }
    }
}
