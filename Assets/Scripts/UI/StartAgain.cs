using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAgain : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Animator playerDeath;
    
    public void Start()
    {
        player1.GetComponent<PlayerHealth>().OnPlayerDeath += EndGame;
        player2.GetComponent<PlayerHealth>().OnPlayerDeath += EndGame;
    }
    public void EndGame()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndScene");
    }
}
