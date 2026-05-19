using UnityEngine;

public class EggCollectible : MonoBehaviour, IColectibles
{
    private bool _isCollected = false;

    public void Collect()
    {
        if (_isCollected) return;
        _isCollected = true;

        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
    }
}
