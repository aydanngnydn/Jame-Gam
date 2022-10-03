using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class WallBulletDestroy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("1");
        if (col.gameObject.layer == 6)
        {
            Debug.Log("2");
            Destroy(col.gameObject);
        }
    }
}
