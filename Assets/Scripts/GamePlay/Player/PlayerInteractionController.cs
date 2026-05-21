using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform playerVisualTransform;
    private PlayerController playerController;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IColectibles>(out var collectible))
        {
            collectible.Collect();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IBoostables>(out var boostable))
        {
            boostable.Boost(playerController);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(playerRigidbody, playerVisualTransform);
            CameraShake.Instance.ShakeCamera(.5f, .5f);
        }
    }
}
