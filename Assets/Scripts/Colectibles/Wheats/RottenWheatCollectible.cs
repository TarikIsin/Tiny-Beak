using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, IColectibles
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float moveDecreasedSpeed;
    [SerializeField] private float resetBoostDuration;


    public void Collect()
    {
        controller.SetMovementSpeed(moveDecreasedSpeed, resetBoostDuration);
        Destroy(gameObject);
    }
}
