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
    [SerializeField] private Transform canvasObject;

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
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), box.GetComponent<Collider2D>());
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
        
        healthMode.IncreaseHealth(4);
    }

    IEnumerator IHealthIncrease()
    {
        var text = Instantiate(increaseText, canvasObject.transform.position + new Vector3(0, 300, 0), Quaternion.identity);
        text.transform.SetParent(canvasObject);
        text.gameObject.transform.Translate(Vector3.up * Time.deltaTime);
        yield return new WaitForSeconds(2);
        Destroy(text);
    }

    void DamageMode()
    {
        StartCoroutine(IHealthDecrease());
        healthMode.DecraseHealth(4);
    }

    IEnumerator IHealthDecrease()
    {
        var text = Instantiate(decreaseText, canvasObject.transform.position + new Vector3(0, 300, 0), Quaternion.identity);
        text.transform.SetParent(canvasObject);
        text.gameObject.transform.Translate(Vector3.up * Time.deltaTime);
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
                yield return new WaitForSeconds(10f);
                playerMovement.doubleJumpMode = !playerMovement.doubleJumpMode;
            }
            else
            {
                yield return new WaitForSeconds(10f);
                playerMovement.doubleJumpMode = !playerMovement.doubleJumpMode;
            }
        }
    }

    IEnumerator IDoubleJump()
    {
        var text = Instantiate(doubleText, canvasObject.transform.position + new Vector3(0, 300, 0), Quaternion.identity);
        text.transform.SetParent(canvasObject);
        text.gameObject.transform.Translate(Vector3.up * Time.deltaTime);
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
                yield return new WaitForSeconds(10f);
                isTripleMode = !isTripleMode;
                defaultMode.enabled = true;
                tripleMode.enabled = false;
            }
            else
            {
                yield return new WaitForSeconds(10f);
                isTripleMode = !isTripleMode;
                defaultMode.enabled = true;
                tripleMode.enabled = false;
            }
        }
    }

    IEnumerator ITriple()
    {
        var text = Instantiate(tripleText, canvasObject.transform.position + new Vector3(0, 300, 0), Quaternion.identity);
        text.transform.SetParent(canvasObject);
        text.gameObject.transform.Translate(Vector3.up * Time.deltaTime);
        yield return new WaitForSeconds(2);
        Destroy(text);
    }
}