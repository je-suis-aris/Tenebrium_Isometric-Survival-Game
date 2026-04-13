using UnityEngine;

public class ZAttackState : StateMachineBehaviour
{
    EnemyManager enemy;
    PlayerState playerState;
    float attackTimer; 
    public float timeBetweenAttacks = 1.5f; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyManager>();
        enemy.agent.ResetPath(); 

        if (enemy.player != null)
            playerState = enemy.player.GetComponent<PlayerState>();

        attackTimer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy == null) return; 

        if (enemy.player != null) enemy.transform.LookAt(enemy.player);

        if (enemy.DistanceToPlayer() > enemy.attackRange || (playerState != null && playerState.isDead))
        {
            animator.SetBool("isAttacking", false);
            return;
        }

        if (attackTimer <= 0)
        {
            if (playerState != null)
            {
                playerState.TakeDamage(enemy.damageAmount);
                
                
                enemy.PlayOneShotSound(enemy.attackSound);
            }
            attackTimer = timeBetweenAttacks;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }
}