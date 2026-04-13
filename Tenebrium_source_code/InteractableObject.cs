using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Configurare")]
    [Tooltip("Numele trebuie sa fie IDENTIC cu cel din lista InventorySystem")]
    public string ItemName; 

    [Header("Audio")]
    [Tooltip("Sunetul care se aude cand ridici obiectul")]
    public AudioClip collectSound; 
    [Range(0f, 1f)]
    public float soundVolume = 0.7f; 

    public void OnInteract()
    {
        Debug.Log("Incerc sa ridic: " + ItemName);

        
        bool wasAdded = InventorySystem.Instance.AddToInventory(ItemName);

        
        if (wasAdded)
        {
            
            if (collectSound != null)
            {
                
                
                if (Camera.main != null)
                {
                    AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position, soundVolume);
                }
                else
                {
                    
                    AudioSource.PlayClipAtPoint(collectSound, transform.position, soundVolume);
                }
            }
            

            Debug.Log("Succes! Am ridicat: " + ItemName);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("EROARE: Nu s-a putut adauga in inventar (Plin?).");
        }
    }
}