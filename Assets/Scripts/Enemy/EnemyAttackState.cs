using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : StateMachineBehaviour
{
    EnemyAI enemy;
    float time = 1;
    float timer;

    float attackEndTime = 1;
    float attackEndTimer = 1;

    bool soundplayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyAI>();

        timer = time;
        attackEndTimer = attackEndTime;

        enemy.attackLight.SetActive(true);

        soundplayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(timer > 0)
       {
            timer -= Time.deltaTime;
       }

       if(timer < time / 12 && !soundplayed)
        {
            enemy.PlayAttacksound();
            soundplayed = true;
        }

       if(timer <= 0 && attackEndTimer > 0)
       {
            enemy.Attack();

            attackEndTimer -= Time.deltaTime;
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.atkDelay = enemy.atkCooltime;
    }
}
