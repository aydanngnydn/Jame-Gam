using UnityEngine;

public interface IPlayerState
{
    void EnterState(PlayerStateManager player);
    void ExecuteState(PlayerStateManager player);
    void OnCollisionEnter(PlayerStateManager player, Collision collision);
}