using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backState : StateMachineBehaviour
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
        if (Vector2.Distance(enemy.backToPosition, enemyTransform.position) < 0.1f || Vector2.Distance(enemyTransform.position, enemy.player.position) < 4f)
        {
            animator.SetBool("isBack", false);
        }
        else
        {
            enemy.direction(enemy.backToPosition.x, enemyTransform.position.x);
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, enemy.backToPosition, Time.deltaTime * enemy.movementSpeed);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
