using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))] 
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Camera mainCamera;
    private AudioSource audioSource; 

    
    private bool isTakingDamage = false;

    [Header("Sunete Pasi")]
    public AudioClip[] footstepSounds; 
    public float walkStepInterval = 0.5f; 
    public float runStepInterval = 0.3f;  
    
    private float stepTimer; 

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
        mainCamera = Camera.main;

        navMeshAgent.updateRotation = true;
        navMeshAgent.angularSpeed = 360f; 
    }

    private void Update()
    {
        
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        
        if (isTakingDamage) return;

        
        float currentSpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("Speed", currentSpeed, 0.2f, Time.deltaTime);

        
        HandleFootsteps(currentSpeed);
        

        
        if (Input.mousePresent)
        {
            HandleMouseClick();
        }
        else if (Input.touchSupported)
        {
            HandleTouch();
        }
    }

    
    
    
    private void HandleFootsteps(float speed)
    {
        
        if (speed < 0.1f) 
        {
            return;
        }

        
        stepTimer -= Time.deltaTime;

        if (stepTimer <= 0)
        {
            
            if (footstepSounds.Length > 0)
            {
                AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
                
                
                audioSource.pitch = Random.Range(0.8f, 1.1f);
                audioSource.PlayOneShot(clip); 
            }

            
            
            if (speed > 3.5f)
            {
                stepTimer = runStepInterval;
            }
            else
            {
                stepTimer = walkStepInterval;
            }
        }
    }

    
    
    

    public void TakeDamageAnimation()
    {
        if (isTakingDamage) return;

        Debug.Log("Player a primit damage!");
        
        navMeshAgent.ResetPath();
        navMeshAgent.velocity = Vector3.zero;
        
        animator.SetTrigger("GetHit");
        isTakingDamage = true;
        animator.SetFloat("Speed", 0f);

        StopAllCoroutines();
        StartCoroutine(RecoverFromDamage(0.5f)); 
    }

    private System.Collections.IEnumerator RecoverFromDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        isTakingDamage = false;
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MovePlayerToPosition(Input.mousePosition);
        }
    }

    private void HandleTouch()
    {
        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(0);
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        {
            MovePlayerToPosition(touch.position);
        }
    }

    private void MovePlayerToPosition(Vector3 screenPosition)
    {
        if (mainCamera == null) return;

        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}