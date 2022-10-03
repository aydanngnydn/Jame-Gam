using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThhroughPlatforms : MonoBehaviour
{
    private Collider2D collider;
    private bool player1OnPlatform, player2OnPlatform;
    
   

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (player1OnPlatform && Input.GetKeyDown(KeyCode.S))
        {
            collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
        else if (player2OnPlatform && Input.GetKeyDown(KeyCode.DownArrow))
        {
            collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }

    void SetPlayerOnPlatform(Collision2D other, bool value)
    { 
        var player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null && player.name == "Player1")
        {
            player1OnPlatform = value;
        }
        else if (player != null && player.name == "Player2")
        {
            player2OnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        SetPlayerOnPlatform(col, true);
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        SetPlayerOnPlatform(col, false);
    }
}
