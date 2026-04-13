using UnityEngine;

public class ZIdleState : StateMachineBehaviour
{
    float timer;
    EnemyManager enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        enemy = animator.GetComponent<EnemyManager>();
        
        
        if(enemy != null)
            enemy.PlayLoopingSound(enemy.idleSound);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (enemy == null)
        {
            enemy = animator.GetComponent<EnemyManager>();
            
            if (enemy == null) return; 
        }
        

        timer += Time.deltaTime;

        if (enemy.DistanceToPlayer() < enemy.sightRange)
        {
            animator.SetBool("isChasing", true);
        }

        if (timer > 3)
        {
            animator.SetBool("isWalking", true);
        }
    }
}