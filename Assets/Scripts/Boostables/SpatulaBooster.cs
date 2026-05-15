using System;
using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostables
{
    [Header("Referrences")]
    [SerializeField] private Animator spatulaAnimator;

    [Header("Settings")]
    [SerializeField] private float jumpForce;

    private bool isActivated;
    public void Boost(PlayerController playerController)
    {
        if (isActivated) { return; }

        PlayBoostAnimation();
        Rigidbody rb = playerController.GetPlayerRigidbody();
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
        
        isActivated = true;
        Invoke(nameof(ResetActivation), 0.2f);
    }

    private void PlayBoostAnimation()
    {
        spatulaAnimator.SetTrigger(Consts.OtherAnimations.IsSpatulaJumping);
    }

    private void ResetActivation()
    {
        isActivated = false;
    }
}
