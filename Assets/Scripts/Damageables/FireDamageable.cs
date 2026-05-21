using System;
using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] public float force = 20f;
    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {
        HealthManager.Instance.Damage(1);
        playerRigidbody.AddForce(-playerVisualTransform.forward * force, ForceMode.Impulse);
        Destroy(gameObject);
    }
}
