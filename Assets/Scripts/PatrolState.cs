using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateMachineBehaviour
{
    EnemyAI enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyAI>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.m_target == null)
        {
            enemy.Sight();
            enemy.Listen();
        }

        if (enemy.m_target != null)
        {
            animator.SetBool("IsFollow", true);
            animator.SetBool("IsPatrol", false);
        }

        else if(enemy.m_enemy.velocity == Vector3.zero)
        {
            animator.SetBool("IsPatrol", false);
            animator.SetBool("IsWait", true);
        }

        else if (Vector3.Distance(enemy.player.transform.position, enemy.transform.position) < enemy.attackDistance)
        {
            animator.SetBool("IsPatrol", false);
            animator.SetBool("IsFollow", true);

            enemy.m_target = enemy.player.transform;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
