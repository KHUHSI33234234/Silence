using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBarUI;

    void Awake()
    {
        if (Instance == null) Instance = this;
        currentHealth = maxHealth;
    }

    void Start()
    {
        if (healthBarUI != null)
        {
            healthBarUI.maxValue = maxHealth;
            healthBarUI.value = currentHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);

        if (healthBarUI != null)
            healthBarUI.value = currentHealth;

        Debug.Log("Player health: " + currentHealth);

        if (currentHealth <= 0f && GameManager.Instance != null)
            GameManager.Instance.EndGame(false); // engaging the enemy to death = trapped ending
    }
}