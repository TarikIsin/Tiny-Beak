using UnityEngine;

public class EggCollectible : MonoBehaviour, IColectibles
{
    private bool _isCollected = false;

    public void Collect()
    {
        if (_isCollected) return;
        _isCollected = true;

        GameManager.Instance.OnEggCollected();
        CameraShake.Instance.ShakeCamera(.2f, .2f);

        Destroy(gameObject);
    }
}
