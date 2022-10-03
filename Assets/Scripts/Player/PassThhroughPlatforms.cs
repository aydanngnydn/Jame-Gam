using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThhroughPlatforms : MonoBehaviour
{
    private GameObject currentOneWayPlatform;

    private BoxCollider2D playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && gameObject.name == "Player1" && currentOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && gameObject.name == "Player2" && currentOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"));
        {
            currentOneWayPlatform = col.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"));
        {
           currentOneWayPlatform = null;
        }
    }

    IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(5f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
