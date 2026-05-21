using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public event Action OnPlayerDeath;

    [Header("References")]
    [SerializeField] private PlayerHealthUI healthUI;

    [Header("Settings")]
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthUI.AnimateDamage();
            if (currentHealth <= 0) 
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }

    public void Heal(int healAmount) 
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        }
    }
}
