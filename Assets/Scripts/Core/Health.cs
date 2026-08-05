// WaruKit — Health
// Vida reutilizable con eventos (patron de Waru: delegados pa' desacoplar).
// Implementa IDamageable/IHealable; otros sistemas escuchan los eventos.
// Uso: health.TakeDamage(10f);  |  health.OnDied += () => ...;
using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    // Eventos pa' que el resto se entere sin acoplarse
    public event Action<float> OnDamaged;   // recibe el dano
    public event Action<float> OnHealed;    // recibe la curacion
    public event Action OnDied;             // sin parametros

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    public bool IsDead => currentHealth <= 0f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return; // ya muerto, no recibir mas dano

        currentHealth -= damage;
        OnDamaged?.Invoke(damage); // null-conditional, nunca revienta sin suscriptores

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (IsDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHealed?.Invoke(amount);
    }

    private void Die()
    {
        OnDied?.Invoke();
        // Aca puedes agregar animaciones de muerte, efectos, etc.
        Destroy(gameObject);
    }
}
