using UnityEngine;
using UnityEngine.UI;

public class HolyWheatCollectible : MonoBehaviour, IColectibles
{
    [SerializeField] private WheatDesignSO wheatDesignSO;
    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerStateUI playerStateUI;

    private RectTransform playerBoosterTransform;
    private Image playerBoosterImage;

    private void Awake()
    {
        playerBoosterTransform = playerStateUI.GetBoosterJumpTransform;
        playerBoosterImage = playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        controller.SetJumpForce(wheatDesignSO.IncreaseDecreaseMultiplier,
            wheatDesignSO.ResetBoostDuration);

        playerStateUI.PlayBoosterUIAnimations(playerBoosterTransform, playerBoosterImage,
            playerStateUI.GetHolyBoosterWheatImage, wheatDesignSO.ActiveSprite,
            wheatDesignSO.PassiveSprite, wheatDesignSO.ActiveWheatSprite,
            wheatDesignSO.PassiveWheatSprite, wheatDesignSO.ResetBoostDuration);

        CameraShake.Instance.ShakeCamera(.2f, .2f);

        Destroy(gameObject);
    }
}
