using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float forceIncrease;
    [SerializeField] private float resetBoostDuration;


    public void Collect()
    {
        controller.SetJumpForce(forceIncrease, resetBoostDuration);
        Destroy(gameObject);
    }
}
