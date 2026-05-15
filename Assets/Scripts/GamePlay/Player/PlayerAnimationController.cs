using System;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private PlayerController playerContoller;
    private StateController stateController;

    private void Awake()
    {
        playerContoller = GetComponent<PlayerController>();
        stateController = GetComponent<StateController>();
    }

    private void Start()
    {
        playerContoller.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }

    private void Update()
    {
        SetPlayerAnimations();
    }

    private void PlayerController_OnPlayerJumped()
    {
        playerAnimator.SetBool(Consts.PlayerAnimations.IsJumping, true);
        Invoke(nameof(PlayerController_OnPlayerLanded), 0.5f);
    }

    private void PlayerController_OnPlayerLanded()
    {
        playerAnimator.SetBool(Consts.PlayerAnimations.IsJumping, false);
    }

    private void SetPlayerAnimations()
    {
        var currentState = stateController.GetCurrentState();
        switch (currentState)
        {
            case PlayerState.Idle:
                playerAnimator.SetBool(Consts.PlayerAnimations.IsSliding, false);
                playerAnimator.SetBool(Consts.PlayerAnimations.IsMoving, false);
                break;

            case PlayerState.Move:
                playerAnimator.SetBool(Consts.PlayerAnimations.IsMoving, true);
                playerAnimator.SetBool(Consts.PlayerAnimations.IsSliding, false);
                break;

            case PlayerState.SlideIdle:
                playerAnimator.SetBool(Consts.PlayerAnimations.IsSlidingActive, false);
                playerAnimator.SetBool(Consts.PlayerAnimations.IsSliding, true);
                break;

            case PlayerState.Slide:
                playerAnimator.SetBool(Consts.PlayerAnimations.IsSlidingActive, true);
                playerAnimator.SetBool(Consts.PlayerAnimations.IsSliding, true);
                break;
        }
    }
}
