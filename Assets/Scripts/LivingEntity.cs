using UnityEngine;
using System; // Necesario para Action

public class LivingEntity : MonoBehaviour
{
    [Header("Health Settings")]
    public float startingHealth;
    protected float health;

    // Evento de muerte (no estático, para que cada entidad pueda tener su propio evento)
    public event Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void takehit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Lanzar evento de muerte si alguien está suscrito
        OnDeath?.Invoke();

        // Desactivar el objeto (puedes cambiar esto por animación + desactivación si quieres)
        gameObject.SetActive(false);
    }
}
