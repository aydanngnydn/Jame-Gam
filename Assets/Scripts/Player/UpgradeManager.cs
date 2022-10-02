using System;
using System.Collections;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private PlayerHealth healthMode;
    private BulletSpawner defaultMode;
    private TripleShoot tripleMode;
    private PlayerMovement playerMovement;

    private bool isTripleMode = false;

    private Coroutine tripleRoutine;
    private Coroutine doubleJumpRoutine;


    private void Awake()
    {
        healthMode = GetComponent<PlayerHealth>();
        defaultMode = GetComponentInChildren<BulletSpawner>();
        tripleMode = GetComponentInChildren<TripleShoot>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.TryGetComponent(out BoxType box))
        {
            switch (box.currentState)
            {
                case States.health:
                    HealthMode();
                    break;
                case States.damage:
                    DamageMode();
                    break;
                case States.jump:
                    DoubleJump();
                    break;
                case States.triple:
                    TripleShoot();
                    break;
            }

            box.DestroyBox();
        }
    }

    void HealthMode()
    {
        healthMode.IncreaseHealth(1);
    }

    void DamageMode()
    {
        healthMode.DecraseHealth(1);
    }

    private void DoubleJump()
    {
        if (doubleJumpRoutine != null)
        {
            StopCoroutine(doubleJumpRoutine);
        }
        doubleJumpRoutine = StartCoroutine(HandleDoubleJumpMode());

        IEnumerator HandleDoubleJumpMode()
        {
            if (!playerMovement.doubleJumpMode)
            {
                playerMovement.doubleJumpMode = !playerMovement.doubleJumpMode;
                yield return new WaitForSeconds(5f);
                playerMovement.doubleJumpMode = !playerMovement.doubleJumpMode;
            }
            else
            {
                yield return new WaitForSeconds(5f);
                playerMovement.doubleJumpMode = !playerMovement.doubleJumpMode;
            }
        }
    }

    void TripleShoot()
    {
        if(tripleRoutine != null)
        {
            StopCoroutine(tripleRoutine);
        }
        
        tripleRoutine = StartCoroutine(HandleTripleMode());

        IEnumerator HandleTripleMode()
        {
            if (!isTripleMode)
            {
                isTripleMode = !isTripleMode;
                defaultMode.enabled = false;
                tripleMode.enabled = true;
                yield return new WaitForSeconds(5f);
                isTripleMode = !isTripleMode;
                defaultMode.enabled = true;
                tripleMode.enabled = false;
            }
            else
            {
                yield return new WaitForSeconds(5f);
                isTripleMode = !isTripleMode;
                defaultMode.enabled = true;
                tripleMode.enabled = false;
            }
        }
    }
}