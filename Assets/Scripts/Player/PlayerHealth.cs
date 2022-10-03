using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [SerializeField] private GameObject bulletDestroyEffect;
    [SerializeField] private float bulletDestroyTime;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI scoreTextGame;

    private ParticleSystem bloodParticle;

    private Collider2D collider2D;

    public event Action OnHealthDecrease;
    public event Action OnPlayerDeath;

    private void Awake()
    {
        bloodParticle = GetComponentInChildren<ParticleSystem>();
        collider2D = GetComponent<Collider2D>();
        ResetHealth();
    }

    protected void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void DecraseHealth(int damage)
    {
        currentHealth -= damage;

        if (!AliveCheck())
        {
            OnPlayerDeath?.Invoke();
            StartCoroutine(WinScene(this.gameObject.name));
            //gameObject.GetComponent<Collider2D>().SetActive(false);
        }
        else
        {
            HandleDamageEvents();
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


    private void HandleDamageEvents()
    {
        OnHealthDecrease?.Invoke();
        bloodParticle.Play();
    }

    private IEnumerator WinScene(string name)
    {

        yield return new WaitForSeconds(1.5f);
        winPanel.SetActive(true);
        scoreTextGame.text = name + " has won!";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}