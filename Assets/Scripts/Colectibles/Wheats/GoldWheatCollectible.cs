using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, IColectibles
{
    [SerializeField] private WheatDesignSO wheatDesignSO;
    [SerializeField] private PlayerController controller;

    public void Collect()
    {
        controller.SetMovementSpeed(wheatDesignSO.IncreaseDecreaseMultiplier,
            wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
