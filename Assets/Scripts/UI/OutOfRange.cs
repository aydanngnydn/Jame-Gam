using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfRange : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
