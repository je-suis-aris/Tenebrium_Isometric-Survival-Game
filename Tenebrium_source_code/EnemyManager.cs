using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class EnemyManager : MonoBehaviour
{
    [Header("Componente")]
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;
    public AudioSource audioSource;

    [Header("Statistici Lupta")]
    public float infectionLevel = 100f;
    public Slider healthBar;
    public GameObject humanPrefab;
    
    [Tooltip("Prefab-ul cu particule care apare cand zombiul se transforma.")]
    public GameObject cureVfxPrefab;
    

    [Header("Statistici Zombie")]
    public float sightRange = 10f;
    public float attackRange = 1.5f;
    public float walkSpeed = 1.5f;
    public float runSpeed = 4.0f;
    public float damageAmount = 5f;

    [Header("Sunete")]
    public AudioClip idleSound;
    public AudioClip chaseSound;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip cureSound;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        if (player == null && GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;

        

        if (healthBar != null)
        {
            healthBar.maxValue = 100f;
            healthBar.value = infectionLevel;
        }
    }

    public void TakeCure(float amount)
    {
        infectionLevel -= amount;
        
        if (healthBar != null) healthBar.value = infectionLevel;

        PlayOneShotSound(hitSound);

        
        if (infectionLevel > 0)
        {
            animator.SetTrigger("GetHit");
        }

        if (infectionLevel <= 0)
        {
            Cured();
        }
    }

    void Cured()
    {
        
        if (cureSound != null)
        {
            AudioSource.PlayClipAtPoint(cureSound, transform.position, 1f);
        }

        
        if (cureVfxPrefab != null)
        {
            
            GameObject vfx = Instantiate(cureVfxPrefab, transform.position, transform.rotation);
            
            
            
            Destroy(vfx, 3f);
        }
        

        
        if (humanPrefab != null)
        {
            Instantiate(humanPrefab, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }

    public void PlayLoopingSound(AudioClip clip)
    {
        if (clip == null) return;
        if (audioSource.clip == clip && audioSource.isPlaying) return;

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayOneShotSound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public float DistanceToPlayer()
    {
        if (player == null) return 999f;
        return Vector3.Distance(transform.position, player.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}