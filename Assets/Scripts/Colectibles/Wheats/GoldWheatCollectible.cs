using UnityEngine;
using UnityEngine.UI;

public class GoldWheatCollectible : MonoBehaviour, IColectibles
{
    [SerializeField] private WheatDesignSO wheatDesignSO;
    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerStateUI playerStateUI;

    private RectTransform playerBoosterTransform;
    private Image playerBoosterImage;

    private void Awake()
    {
        playerBoosterTransform = playerStateUI.GetBoosterSpeedTransform;
        playerBoosterImage = playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        controller.SetMovementSpeed(wheatDesignSO.IncreaseDecreaseMultiplier,
            wheatDesignSO.ResetBoostDuration);

        playerStateUI.PlayBoosterUIAnimations(playerBoosterTransform, playerBoosterImage,
            playerStateUI.GetGoldBoosterWheatImage, wheatDesignSO.ActiveSprite,
            wheatDesignSO.PassiveSprite, wheatDesignSO.ActiveWheatSprite,
            wheatDesignSO.PassiveWheatSprite, wheatDesignSO.ResetBoostDuration);

        Destroy(gameObject);
    }
}
