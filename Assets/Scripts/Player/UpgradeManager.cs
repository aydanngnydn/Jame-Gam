using System;
using System.Collections;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private Animator boxAnim;
    private PlayerHealth healthMode;
    private DefaultShoot defaultMode;
    private TripleShoot tripleMode;
    private PlayerMovement playerMovement;

    private bool isTripleMode = false;

    private Coroutine TripleRoutine;
    private Coroutine DoubleJumpRoutine;


    private void Awake()
    {
        healthMode = GetComponent<PlayerHealth>();
        defaultMode = GetComponentInChildren<DefaultShoot>();
        tripleMode = GetComponentInChildren<TripleShoot>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;

        if (gameObject.TryGetComponent(out UpgradeBox box))
        {
            boxAnim = box.GetComponent<Animator>();
            boxAnim.SetTrigger("BoxOpen");

            switch (box.currentState)
            {
                case UpgradeTypes.HealthIncrease:
                    HealthMode();
                    break;
                case UpgradeTypes.HealthDecrease:
                    DamageMode();
                    break;
                case UpgradeTypes.DoubleJump:
                    DoubleJump();
                    break;
                case UpgradeTypes.TripleShoot:
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
        if (DoubleJumpRoutine != null)
        {
            StopCoroutine(DoubleJumpRoutine);
        }

        DoubleJumpRoutine = StartCoroutine(HandleDoubleJumpMode());

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
        if (TripleRoutine != null)
        {
            StopCoroutine(TripleRoutine);
        }

        TripleRoutine = StartCoroutine(HandleTripleMode());

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