using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class HumanSurvivorAI : MonoBehaviour
{
    [Header("Comportament Uman")]
    public float walkRadius = 20f;
    public float walkSpeed = 1.5f;
    public float stopDistanceToPlayer = 3f;

    [Header("Timpi de Așteptare")]
    public float minWaitTime = 3f;
    public float maxWaitTime = 8f;
    public float initialRecoveryTime = 4f;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;
    
    private float timer;
    private bool isRecovering = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        agent.speed = walkSpeed;

        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;

        timer = initialRecoveryTime;
    }

    void Update()
    {
        
        if (isRecovering)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isRecovering = false;
                MoveToRandomPoint();
            }
            return;
        }

        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer < stopDistanceToPlayer)
        {
            agent.isStopped = true;
            
            animator.SetBool("isWalking", false); 
            
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            
            return;
        }
        else
        {
            agent.isStopped = false;
        }

        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            
            animator.SetBool("isWalking", false);

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                MoveToRandomPoint();
                timer = Random.Range(minWaitTime, maxWaitTime);
            }
        }
        else
        {
            
            animator.SetBool("isWalking", true);
        }
    }

    void MoveToRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}