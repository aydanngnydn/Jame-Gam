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
    }

    private void OnDisable()
    {
        _player1Health.OnHealthDecrease -= DestroyHealthbar1;
        _player1Health.OnPlayerDeath -= DestroyHealthbar1;
        _player2Health.OnHealthDecrease -= DestroyHealthbar2;
        _player2Health.OnPlayerDeath -= DestroyHealthbar2;
    }
    void DestroyHealthbar1()
    {
        Destroy(healths1[index1]);
        index1++;
    }
    void DestroyHealthbar2()
    {
        Destroy(healths2[index2]);
        index2++;
    }
}
