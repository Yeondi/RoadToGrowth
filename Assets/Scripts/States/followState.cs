using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followState : StateMachineBehaviour
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
        if (Vector2.Distance(enemy.player.position, enemyTransform.position) > 5.5f)
        {
            animator.SetBool("isBack", true);
            animator.SetBool("isFollow", false);
        }
        else if (Vector2.Distance(enemy.player.position, enemyTransform.position) > 3f)
        {
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, enemy.player.position, Time.deltaTime * enemy.movementSpeed);
        }
        else
        {
            animator.SetBool("isFollow", false);
            animator.SetBool("isBack", false);
        }
        enemy.direction(enemy.player.position.x, enemyTransform.position.x);
    }
}
