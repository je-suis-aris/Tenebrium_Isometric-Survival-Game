using UnityEngine;

public class ZChaseState : StateMachineBehaviour
{
    EnemyManager enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyManager>();
        enemy.agent.speed = enemy.runSpeed;
        
        
        enemy.PlayLoopingSound(enemy.chaseSound);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemy.player != null)
        {
            enemy.agent.SetDestination(enemy.player.position);
        }

        float distance = enemy.DistanceToPlayer();

        
        if (distance < enemy.attackRange)
        {
            animator.SetBool("isAttacking", true);
        }

        
        if (distance > 15f)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isWalking", false); 
            enemy.agent.ResetPath();
        }
    }
}