using System.Collections;
using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    [SerializeField] private Animator catAnimator;
    private CatStateController catStateController;

    private void Awake()
    {
        catStateController = GetComponent<CatStateController>();
    }

    private void Update()
    {
        SetCatAnimations();
    }
    private void SetCatAnimations()
    {
        var currentCatState = catStateController.GetCurrentState();

        switch (currentCatState) 
        {
            case CatState.Idle:
                catAnimator.SetBool(Consts.CatAnimations.IsIdling, true);
                catAnimator.SetBool(Consts.CatAnimations.IsWalking, false);
                catAnimator.SetBool(Consts.CatAnimations.IsRunning, false);
                break;

            case CatState.Walking:
                catAnimator.SetBool(Consts.CatAnimations.IsIdling, false);
                catAnimator.SetBool(Consts.CatAnimations.IsWalking, true);
                catAnimator.SetBool(Consts.CatAnimations.IsRunning, false);
                break;

            case CatState.Running:
                catAnimator.SetBool(Consts.CatAnimations.IsRunning, true);
                break;
            case CatState.Attacking:
                catAnimator.SetBool(Consts.CatAnimations.IsAttacking, true);
                break;
        }
    }
}
