using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("Interactiune")]
    public LayerMask interactableLayer;
    public LayerMask enemyLayer;
    public float interactionRange = 2.5f; 

    [Header("Lupta (Vindecare)")]
    public GameObject potionProjectilePrefab;
    public Transform firePoint;
    public Animator playerAnimator;

    [Header("Audio")]
    public AudioClip throwSound;
    private AudioSource audioSource;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

            HandleRightClick();
        }
    }

    private void HandleRightClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            
            if (((1 << hit.collider.gameObject.layer) & enemyLayer) != 0)
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));

                if (playerAnimator != null) playerAnimator.SetTrigger("Attack");

                ThrowPotion(hit.transform);
            }
            
            else if (((1 << hit.collider.gameObject.layer) & interactableLayer) != 0)
            {
                
                float distance = Vector3.Distance(transform.position, hit.point);

                if (distance <= interactionRange)
                {
                    
                    
                    
                    transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));

                    
                    if (playerAnimator != null) 
                    {
                        playerAnimator.SetTrigger("isHarvesting");
                    }

                    
                    InteractableObject item = hit.transform.GetComponent<InteractableObject>();
                    if (item != null) item.OnInteract();
                }
                else
                {
                    
                    Debug.Log("Prea departe! Apropie-te pentru a colecta.");
                }
            }
        }
    }

    void ThrowPotion(Transform targetEnemy)
    {
        if (potionProjectilePrefab != null && firePoint != null)
        {
            if (throwSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(throwSound);
            }

            GameObject proj = Instantiate(potionProjectilePrefab, firePoint.position, Quaternion.identity);

            
            
            
            CurePotion script = proj.GetComponent<CurePotion>();
            if (script != null)
            {
                script.SeekTarget(targetEnemy);
            }
            
            
        }
    }
}