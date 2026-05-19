using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Referrences")]
    [SerializeField] private Image[] playerHealthImages;
    [Header("Sprites")]
    [SerializeField] private Sprite playerHealthySprite;
    [SerializeField] private Sprite playerUnhealthySprite;
    [Header("Settings")]
    [SerializeField] private float scaleDuration;
    private RectTransform[] playerHealthTransforms;

    private void Awake()
    {
        playerHealthTransforms = new RectTransform[playerHealthImages.Length];

        for (int i = 0; i < playerHealthImages.Length; i++)
        {
            playerHealthTransforms[i] = playerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            AnimateDamage();
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            AnimateDamageForAll();
        }
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < playerHealthImages.Length; i++)
        {
            if (playerHealthImages[i].sprite == playerHealthySprite)
            {
                AnimateDamageSprite(playerHealthImages[i], playerHealthTransforms[i]);
                break;
            }
        }
    }

    public void AnimateDamageForAll()
    {
        for (int i = 0; i < playerHealthImages.Length; i++)
        {
            AnimateDamageSprite(playerHealthImages[i], playerHealthTransforms[i]);
        }
    }

    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = playerUnhealthySprite;
            activeImageTransform.DOScale(1f, scaleDuration).SetEase(Ease.OutBack);
        });
    }

}
