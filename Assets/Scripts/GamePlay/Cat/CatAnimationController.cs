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
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume
            && GameManager.Instance.GetCurrentGameState() != GameState.CutScene
            && GameManager.Instance.GetCurrentGameState() != GameState.GameOver)
        {
            catAnimator.enabled = false;
            return;
        }
        SetCatAnimations();
    }
    private void SetCatAnimations()
    {
        catAnimator.enabled = true;
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
