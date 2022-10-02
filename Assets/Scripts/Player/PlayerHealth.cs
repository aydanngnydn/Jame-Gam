using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [SerializeField] private GameObject bulletDestroyEffect;
    [SerializeField] private float bulletDestroyTime;

    private Collider2D collider2D;

    public event Action OnHealthDecrease;
    public event Action OnPlayerDeath;

    protected virtual void OnEnable()
    {
        ResetHealth();
    }

    protected void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    public void DecraseHealth(int damage)
    {
        currentHealth -= damage;

        if (!AliveCheck())
        {
            OnPlayerDeath?.Invoke();
            Destroy(collider2D);
            Destroy(gameObject, 0.5f);
        }
        else
        {
            OnHealthDecrease?.Invoke();
        }
    }

    public void IncreaseHealth(int refill)
    {
        currentHealth += refill;
    }

    public bool AliveCheck()
    {
        return currentHealth > 0;
    }

    public void CreateBulletDestroyEffect(Vector2 contactPoint)
    {
        GameObject destroyEffect = Instantiate(bulletDestroyEffect, contactPoint, Quaternion.identity);
        Destroy(destroyEffect, bulletDestroyTime);
    }
}