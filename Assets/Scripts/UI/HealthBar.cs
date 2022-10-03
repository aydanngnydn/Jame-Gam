using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _player1Health;
    [SerializeField] private PlayerHealth _player2Health;
    [SerializeField] private GameObject[] healths1;
    [SerializeField] private GameObject[] healths2;
    private int index1 = 0;
    private int index2 = 0;

    [SerializeField] private int healthPerPlayer1;
    [SerializeField] private int healthPerPlayer2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnEnable()
    {
        _player1Health.OnHealthDecrease += DestroyHealthbar1;
        _player1Health.OnPlayerDeath += DestroyHealthbar1;
        _player2Health.OnHealthDecrease += DestroyHealthbar2;
        _player2Health.OnPlayerDeath += DestroyHealthbar2;
        
        _player1Health.OnHealthIncrease += IncreaseHealthBar1;
        _player2Health.OnHealthIncrease += IncreaseHealthBar2;
    }

    private void OnDisable()
    {
        _player1Health.OnHealthDecrease -= DestroyHealthbar1;
        _player1Health.OnPlayerDeath -= DestroyHealthbar1;
        _player2Health.OnHealthDecrease -= DestroyHealthbar2;
        _player2Health.OnPlayerDeath -= DestroyHealthbar2;

        _player1Health.OnHealthIncrease -= IncreaseHealthBar1;
        _player2Health.OnHealthIncrease -= IncreaseHealthBar2;
    }

    private void IncreaseHealthBar1()
    {
        if(index1 != 0) healths1[index1 - 1].SetActive(true);
    }

    private void IncreaseHealthBar2()
    {
        if (index2 != 0) healths2[index2 - 1].SetActive(true);
    }

    void DestroyHealthbar1()
    {
        var health = _player1Health.GetHealth();
        if ( health % 4 == 0 && healths1[index1] != null)
        {
            healths1[index1].SetActive(false);
            index1++;
        }
    }
    void DestroyHealthbar2()
    {
        var health = _player2Health.GetHealth();
        if (health % 4 == 0 && healths2[index2] != null)
        {
            healths2[index2].SetActive(false);
            index2++;
        }
    }
}
