using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : StateMachineBehaviour
{
    EnemyAI enemy;

    float time = 3;
    float timer = 0;
    bool prevPlayerState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyAI>();

        prevPlayerState = enemy.playerBehindObstacle;

        timer = time;

        enemy.m_enemy.speed = enemy.followSpeed;

        enemy.growlingsound();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemy.m_target != null)
        {
            enemy.m_enemy.SetDestination(enemy.m_target.position);
        }

        //enemy.Sight_Obstacle();

        //Debug.Log(enemy.playerBehindObstacle + timer.ToString());

        //if (enemy.playerBehindObstacle && timer <= 0)
        //{
        //    animator.SetBool("IsPatrol", true);
        //    animator.SetBool("IsFollow", false);

        //    enemy.RemoveTarget();

        //    enemy.FindNearestWayPoint();
        //    enemy.StartPatrol();
        //}

        if(enemy.m_target != null)
        {
            if (Vector3.Distance(enemy.m_target.transform.position, enemy.transform.position) > enemy.detect_distance)
            {
                animator.SetBool("IsPatrol", true);
                animator.SetBool("IsFollow", false);

                enemy.RemoveTarget();

                enemy.FindNearestWayPoint();
                enemy.StartPatrol();
            }

            else if (Vector3.Distance(enemy.player.transform.position, enemy.transform.position) < enemy.attackDistance)
            {
                animator.SetBool("IsPatrol", false);
                animator.SetBool("IsFollow", false);

                enemy.m_enemy.ResetPath();
            }

            //
            else if (Vector3.Distance(enemy.m_target.transform.position, enemy.transform.position) < 1.0f)
            {
                enemy.RemoveTarget();

                //enemy.FindNearestWayPoint();
                enemy.StartPatrol();

                animator.SetBool("IsPatrol", true);
                animator.SetBool("IsFollow", false);
            }
        }

        else
        {
            enemy.RemoveTarget();

            //enemy.FindNearestWayPoint();
            enemy.StartPatrol();

            animator.SetBool("IsPatrol", true);
            animator.SetBool("IsFollow", false);
        }

        
        
        //

        //if (enemy.playerBehindObstacle)
        //{
        //    timer -= Time.deltaTime;
        //}

        //else if (!enemy.playerBehindObstacle)
        //{
        //    timer = time;
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.FindNearestWayPoint();
    }
}
