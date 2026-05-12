using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState currentPlayerState = PlayerState.Idle;


    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }

    public void ChangeState(PlayerState newState)
    {
        if(currentPlayerState == newState) return;  
        currentPlayerState = newState;
    }

    public PlayerState GetCurrentState()
    {
        return currentPlayerState;
    }
}
