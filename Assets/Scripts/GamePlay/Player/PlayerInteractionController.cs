using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IColectibles>(out var collectible))
        {
            collectible.Collect();
        }
    }
}
