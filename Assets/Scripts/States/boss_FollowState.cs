using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_FollowState : StateMachineBehaviour
{
    Boss boss;
    Transform bossTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        bossTransform = animator.GetComponent<Boss>().transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(boss.player.position, bossTransform.position) > 5.5f)
        {
            animator.SetBool("isWander", true);
            animator.SetBool("isFollow", false);
        }
        else if (Vector2.Distance(boss.player.position, bossTransform.position) > 3f)
        {
            bossTransform.position = Vector2.MoveTowards(bossTransform.position, boss.player.position, Time.deltaTime * boss.movementSpeed);
        }
        else
        {
            animator.SetBool("isFollow", false);
            animator.SetBool("isWander", false);
        }
        boss.direction(boss.player.position.x, bossTransform.position.x);
    }

}
