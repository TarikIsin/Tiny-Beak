using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private RectTransform playerWalkingTransform;
    [SerializeField] private RectTransform playerSlidingTransform;
    [SerializeField] private RectTransform boosterSpeedTransform;
    [SerializeField] private RectTransform boosterJumpTransform;
    [SerializeField] private RectTransform boosterSlowTransform;
    [SerializeField] private PlayableDirector playableDirector;

    [Header("Images")]
    [SerializeField] private Image goldBoosterWheatImage;
    [SerializeField] private Image holyBoosterWheatImage;
    [SerializeField] private Image rottenBoosterWheatImage;

    [Header("Sprites")]
    [SerializeField] private Sprite playerWalkingActiveSprite;
    [SerializeField] private Sprite playerWalkingPassiveSprite;
    [SerializeField] private Sprite playerSlidingActiveSprite;
    [SerializeField] private Sprite playerSlidingPassiveSprite;

    [Header("Settings")]
    [SerializeField] private float moveDuration;
    [SerializeField] private Ease moveEase;

    public RectTransform GetBoosterSpeedTransform => boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => boosterSlowTransform;

    public Image GetGoldBoosterWheatImage => goldBoosterWheatImage;
    public Image GetHolyBoosterWheatImage => holyBoosterWheatImage;
    public Image GetRottenBoosterWheatImage => rottenBoosterWheatImage;

    private Image playerWalkingImage;
    private Image playerSlidingImage;

    private void Awake()
    {
        playerWalkingImage = playerWalkingTransform.GetComponent<Image>();
        playerSlidingImage = playerSlidingTransform.GetComponent<Image>();
    }
    private void Start()
    {
        playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;
        playableDirector.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        SetStateUserInterfaces(playerWalkingActiveSprite, playerSlidingPassiveSprite,
                    playerWalkingTransform, playerSlidingTransform);
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState state)
    {
        switch (state) 
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterfaces(playerWalkingActiveSprite, playerSlidingPassiveSprite,
                    playerWalkingTransform, playerSlidingTransform);
                break;

            case PlayerState.SlideIdle:
            case PlayerState.Slide:
                SetStateUserInterfaces(playerWalkingPassiveSprite, playerSlidingActiveSprite,
                    playerSlidingTransform, playerWalkingTransform);
                break;
        }
    }

    private void SetStateUserInterfaces(Sprite playerWalking, Sprite playerSliding,
        RectTransform activeTransform, RectTransform passiveTransform)
    {
        playerWalkingImage.sprite = playerWalking;
        playerSlidingImage.sprite = playerSliding;

        activeTransform.DOAnchorPosX(-25f, moveDuration).SetEase(moveEase);
        passiveTransform.DOAnchorPosX(-90f, moveDuration).SetEase(moveEase);
    }

    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImage,
        Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite,
        Sprite passiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, moveDuration).SetEase(moveEase);

        yield return new WaitForSeconds(duration);

        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, moveDuration).SetEase(moveEase);
    }

    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage,
        Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite,
        Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, wheatImage,
            activeSprite, passiveSprite, activeWheatSprite, passiveWheatSprite, duration));
    }
}
