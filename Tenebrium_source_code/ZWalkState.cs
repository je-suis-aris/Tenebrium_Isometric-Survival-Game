using UnityEngine;
using UnityEngine.AI;

public class ZWalkState : StateMachineBehaviour
{
    EnemyManager enemy;
    Vector3 walkPoint;
    bool walkPointSet;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyManager>();
        enemy.agent.speed = enemy.walkSpeed;
        walkPointSet = false;
        
        SearchWalkPoint();
        
        
        enemy.PlayLoopingSound(enemy.idleSound);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (enemy.DistanceToPlayer() < enemy.sightRange)
        {
            animator.SetBool("isChasing", true);
            enemy.agent.ResetPath(); 
            return;
        }

        
        if (walkPointSet)
        {
            enemy.agent.SetDestination(walkPoint);
        }

        
        Vector3 distanceToWalkPoint = enemy.transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-10, 10);
        float randomX = Random.Range(-10, 10);
        Vector3 potentialPoint = new Vector3(enemy.transform.position.x + randomX, enemy.transform.position.y, enemy.transform.position.z + randomZ);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(potentialPoint, out hit, 2f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }
}