using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readyState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.attackDelay <= 4 && enemy.attackDelay <= float.Epsilon)
        {
            animator.SetTrigger("attack");

        }

        if (Vector2.Distance(enemy.player.position, enemyTransform.position) > 4.1f)
            animator.SetBool("isFollow", true);
    }

}
