using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject blackBackgroundObject;
    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject losePopup;

    [Header("Settings")]
    [SerializeField] private float animationDuration = 0.3f;

    private Image blackBackgroundImage;

    private RectTransform winPopupTransform;
    private RectTransform losePopupTransform;

    private void Awake()
    {
        blackBackgroundImage = blackBackgroundObject.GetComponent<Image>();
        winPopupTransform = winPopup.GetComponent<RectTransform>();
        losePopupTransform = losePopup.GetComponent<RectTransform>();
    }

    public void OnGameWin()
    {
        blackBackgroundObject.SetActive(true);
        winPopup.SetActive(true);

        blackBackgroundImage.DOFade(.8f, animationDuration).SetEase(Ease.Linear);
        winPopupTransform.DOScale(1.5f, animationDuration).SetEase(Ease.OutBack);
    }

    public void OnGameLose()
    {
        blackBackgroundObject.SetActive(true);
        losePopup.SetActive(true);

        blackBackgroundImage.DOFade(.8f, animationDuration).SetEase(Ease.Linear);
        losePopupTransform.DOScale(1.5f, animationDuration).SetEase(Ease.OutBack);
    }
}
