using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float moveIncreasedSpeed;
    [SerializeField] private float resetBoostDuration;


    public void Collect()
    {
        controller.SetMovementSpeed(moveIncreasedSpeed, resetBoostDuration);
        Destroy(gameObject);
    }
}
