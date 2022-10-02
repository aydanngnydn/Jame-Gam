using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private BulletSpawner defaultMode;

    public event Action OnDoubleJumpUpgrade;
    public event Action OnTripleShootUpgrade;

   
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
                    OnDoubleJumpUpgrade?.Invoke();
                   break;
               case States.triple:
                   OnTripleShootUpgrade?.Invoke();
                   
                   break;
            }
            box.DestroyBox();
        }
        
    }
}
