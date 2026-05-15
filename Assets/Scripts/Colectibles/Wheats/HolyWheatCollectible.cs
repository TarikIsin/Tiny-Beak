using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, IColectibles
{
    [SerializeField] private WheatDesignSO wheatDesignSO;
    [SerializeField] private PlayerController controller;

    public void Collect()
    {
        controller.SetJumpForce(wheatDesignSO.IncreaseDecreaseMultiplier,
            wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
