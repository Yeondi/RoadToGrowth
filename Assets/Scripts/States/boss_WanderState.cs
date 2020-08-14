using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_WanderState : StateMachineBehaviour
{
    Boss boss;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(boss.transform.position, boss.player.position) <= 4)
        {
            animator.SetBool("isFollow", true);
            animator.SetBool("isWander", false);
        }
        if (boss.wanderDelay <= 0.1f)
        {
            int nRandomBehaviour = Random.Range(0, 4);
            int nRandomDistance = Random.Range(2, 5);
            switch (nRandomBehaviour)
            {
                case 0: //move
                    if (boss.currentDirection == 1)
                    {
                        boss.GetComponent<Rigidbody2D>().velocity = Vector2.right * nRandomDistance;
                        animator.SetFloat("behaviour", nRandomBehaviour);
                    }
                    else if (boss.currentDirection == 0)
                    {
                        boss.GetComponent<Rigidbody2D>().velocity = Vector2.left * nRandomDistance;
                        animator.SetFloat("behaviour", nRandomBehaviour);
                    }
                    break;
                //case 1: // idle
                //    break;
                //case 2: // combat idle
                //    break;
                case 3: // change direction
                    if (boss.currentDirection == 0)
                    {
                        boss.currentDirection = 1;
                        boss.transform.localScale = new Vector3(-5, 5, 0);
                    }
                    else if (boss.currentDirection == 1)
                    {
                        boss.currentDirection = 0;
                        boss.transform.localScale = new Vector3(5, 5, 0);
                    }
                    break;
                default:
                    break;
            }
            boss.wanderDelay = boss.wanderCoolDown;
            //animator.SetFloat("behaviour", nRandomBehaviour);
        }
    }

}
