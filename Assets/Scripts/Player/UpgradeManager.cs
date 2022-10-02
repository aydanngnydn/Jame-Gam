using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private BulletSpawner defaultMode;
    private void Awake()
    {
        defaultMode = GetComponentInChildren<BulletSpawner>();
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
                   defaultMode.enabled = false;
                   GetComponentInChildren<TripleShoot>().enabled = true;
                   break;
            }
            box.DestroyBox();
        }
        
    }
}
