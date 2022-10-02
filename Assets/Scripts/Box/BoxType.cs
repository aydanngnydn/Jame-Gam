using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using Random = UnityEngine.Random;

public enum States{
    health,triple,jump,damage,defaultS
}
public class BoxType : MonoBehaviour
{
    public States currentState;
    private void Awake()
    {
        int rand = 2;//Random.Range(1, 4);
        
        switch (rand)
        {
            case 1:
                currentState = States.health;
                break;
            case 2:
                currentState = States.triple;
                break;
            case 3:
                currentState = States.jump;
                break;
            case 4:
                currentState = States.damage;
                break;
            default:
                currentState = States.defaultS;
                break;
        }
    }
    
    public void DestroyBox()
    {
        Destroy(gameObject);
    }
}
