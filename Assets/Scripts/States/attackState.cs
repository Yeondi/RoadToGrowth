using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackState : StateMachineBehaviour
{
    Enemy enemy;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Weapon weapon = enemy.GetComponent<Weapon>();
        if (enemy.attackDelay <= float.Epsilon)
        {
            weapon.FireAmmo(enemy.player.position);
            enemy.attackDelay = enemy.attackCoolDown;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.nAttackCount = 1;
    }
}
