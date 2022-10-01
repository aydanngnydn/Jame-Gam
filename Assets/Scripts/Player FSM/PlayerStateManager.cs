using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private IPlayerState currentState;
    public PlayerDefaultState DefaultState = new PlayerDefaultState();
    public PlayerDoubleJumpState DoubleJumpState = new PlayerDoubleJumpState();
    public PlayerTripleState TripleState = new PlayerTripleState();
    public PlayerBombState BombState = new PlayerBombState();

    void Start()
    {
        currentState = DefaultState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.ExecuteState(this);
    }

    public void SwitchState(IPlayerState newState)
    {
        currentState = newState;
        newState.EnterState(this);
    }

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
}
