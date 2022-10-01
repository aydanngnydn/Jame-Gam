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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("1");
        GameObject gameObject = collision.gameObject;
        if (gameObject.TryGetComponent(out BoxType box))
        {
            defaultMode.enabled = false;
            
            switch (box.currentState)
            {
               case States.bomb:
                   break;
               case States.damage:
                   break;
               case States.jump:
                   break;
               case States.triple:
                   gameObject.GetComponentInParent<TripleShoot>(enabled);
                   break;
            }
            box.DestroyBox();
        }
        
    }
}
