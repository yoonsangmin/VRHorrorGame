using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : StateMachineBehaviour
{
    EnemyAI enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyAI>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.atkDelay <= 0)
        {
            enemy.needLookAt = true;

            //enemy.transform.LookAt(enemy.player.transform);

            animator.SetTrigger("Attack");

            enemy.attackLight.SetActive(false);
        }
            

        if (Vector3.Distance(enemy.player.transform.position, enemy.transform.position) > enemy.attackDistance)
        {
            animator.SetBool("IsFollow", true);

            enemy.attackLight.SetActive(false);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
