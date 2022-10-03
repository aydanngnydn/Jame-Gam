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
    
    [SerializeField] private GameObject doubleText;
    [SerializeField] private GameObject tripleText;
    [SerializeField] private GameObject increaseText;
    [SerializeField] private GameObject decreaseText;

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
        StartCoroutine(IHealthIncrease());
        
        healthMode.IncreaseHealth(1);
    }

    IEnumerator IHealthIncrease()
    {
        var text = Instantiate(increaseText, this.gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(text);
    }

    void DamageMode()
    {
        StartCoroutine(IHealthDecrease());
        healthMode.DecraseHealth(1);
    }

    IEnumerator IHealthDecrease()
    {
        var text = Instantiate(decreaseText, this.gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(text);
    }

    private void DoubleJump()
    {
        StartCoroutine(IDoubleJump());
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

    IEnumerator IDoubleJump()
    {
        var text = Instantiate(doubleText, this.gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(text);
    }
    void TripleShoot()
    {
        StartCoroutine(ITriple());
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

    IEnumerator ITriple()
    {
        var text = Instantiate(tripleText, this.gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(text);
    }
}