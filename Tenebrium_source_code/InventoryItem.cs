using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Item Data")]
    public string idName;
    public int currentStackSize;
    public int maxStackSize = 32;

    [Header("Consumable Settings")]
    public bool isConsumable;
    public float healthToRestore;
    public AudioClip consumeSound;

    [Header("UI References")]
    public TextMeshProUGUI amountText;

    private bool isHovered = false;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        
        if (isHovered && Input.GetKeyDown(KeyCode.E))
        {
            TryConsume();
        }
    }

    public void AddToStack(int amount)
    {
        currentStackSize += amount;
        UpdateUI();
    }

    public void RemoveFromStack(int amount)
    {
        currentStackSize -= amount;
        UpdateUI();

        if (currentStackSize <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateUI()
    {
        if (amountText == null) return;

        if (currentStackSize > 1)
        {
            amountText.text = currentStackSize.ToString();
            amountText.gameObject.SetActive(true);
        }
        else
        {
            amountText.text = "";
            amountText.gameObject.SetActive(false);
        }
    }

    void TryConsume()
    {
        if (!isConsumable) return;

        
        PlayerState player = FindFirstObjectByType<PlayerState>();
        
        if (player != null)
        {
            
            if (player.currentHealth >= player.maxHealth)
            {
                Debug.Log("Sanatate plina! Nu pot consuma.");
                return; 
            }

            
            player.Heal(healthToRestore);

            
            if (consumeSound != null)
            {
                
                
                if (Camera.main != null)
                {
                    AudioSource.PlayClipAtPoint(consumeSound, Camera.main.transform.position, 1f);
                }
                else
                {
                    
                    AudioSource.PlayClipAtPoint(consumeSound, player.transform.position, 1f);
                }
            }
            
            Debug.Log("Consumat: " + idName);

            
            RemoveFromStack(1);
        }
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}