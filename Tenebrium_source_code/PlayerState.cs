using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))] 
public class PlayerState : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead = false;

    [Header("UI References")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public GameObject gameOverUI;

    [Header("Audio")]
    public AudioClip damageSound; 
    private AudioSource audioSource;

    
    private PlayerController playerController;

    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        UpdateHealthUI();
        
        
        
        
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
        }
        else
        {
            AudioListener.volume = 1f;
        }
        

        if (gameOverUI != null) gameOverUI.SetActive(false);

        
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        if (currentHealth < 0) currentHealth = 0;
        
        UpdateHealthUI();

        
        
        
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        
        if (currentHealth > 0 && playerController != null)
        {
            playerController.TakeDamageAnimation();
        }
        

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        if (isDead) return;

        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        if (healthText != null)
        {
            healthText.text = $"{currentHealth} / {maxHealth}";
        }
    }

    private void Die()
    {
        isDead = true;

        
        if (gameOverUI != null) gameOverUI.SetActive(true);

        
        AudioListener.volume = 0f; 
        Time.timeScale = 0f; 
    }
}