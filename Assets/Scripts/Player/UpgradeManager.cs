using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private BulletSpawner defaultMode;
    private TripleShoot tripleMode;

    private bool isTripleMode = false;

    private Coroutine tripleRoutine;

    //public event Action OnDoubleJumpUpgrade;


    private void Awake()
    {
        defaultMode = GetComponentInChildren<BulletSpawner>();
        tripleMode = GetComponentInChildren<TripleShoot>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.TryGetComponent(out BoxType box))
        {
            switch (box.currentState)
            {
                case States.bomb:
                    break;
                case States.damage:
                    break;
                case States.jump:
                    break;
                case States.triple:
                    TripleShoot();

                    break;
            }

            box.DestroyBox();
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