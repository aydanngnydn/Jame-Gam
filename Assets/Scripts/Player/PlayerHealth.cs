using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int contactDamage;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private Collider2D collider;

    public event Action OnHealthDecrease;
    public event Action OnPlayerDeath;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    public void DecraseHealth(int damage)
    {
        currentHealth -= damage;

        if (!AliveCheck())
        {
            OnPlayerDeath?.Invoke();
            Destroy(collider);
            Destroy(gameObject, 0.5f);
        }
        else
        {
            OnHealthDecrease?.Invoke();
        }
    }

    public bool AliveCheck()
    {
        return currentHealth > 0;
    }
}