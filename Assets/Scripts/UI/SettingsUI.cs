using DG.Tweening;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject settingsPopupObject;
    [SerializeField] private GameObject blackBackgroundObject;

    [Header("Buttons")]
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Sprites")]
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    [Header("Settings")]
    [SerializeField] private float animationDuration;

    private Image blackBackgroundImage;

    private bool isMusicOn = true;
    private bool isSoundOn = true;

    private void Awake()
    {
        blackBackgroundImage = blackBackgroundObject.GetComponent<Image>();
        settingsPopupObject.transform.localScale = Vector3.zero;

        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            SceneManager.LoadScene(Consts.SceneNames.MenuScene);
        });

        musicButton.onClick.AddListener(OnMusicButtonClicked);
        soundButton.onClick.AddListener(OnSoundButtonClicked);
    }

    private void OnMusicButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        isMusicOn = !isMusicOn;
        musicButton.image.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
        BackgroundMusic.Instance.SetMusicMute(!isMusicOn);
    }

    private void OnSoundButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        isSoundOn = !isSoundOn;
        soundButton.image.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        AudioManager.Instance.SetSoundEffectsMute(!isSoundOn);
    }

    private void OnSettingsButtonClicked()
    {
        GameManager.Instance.ChangeGameState(GameState.Pause);
        AudioManager.Instance.Play(SoundType.ButtonClickSound);

        blackBackgroundObject.SetActive(true);
        settingsPopupObject.SetActive(true);

        blackBackgroundImage.DOFade(.8f, animationDuration).SetEase(Ease.Linear);
        settingsPopupObject.transform.DOScale(1.5f, animationDuration).SetEase(Ease.OutBack);
    }

    private void OnResumeButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        blackBackgroundImage.DOFade(0f, animationDuration).SetEase(Ease.Linear);
        settingsPopupObject.transform.DOScale(0f, animationDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);
            blackBackgroundObject.SetActive(false);
            settingsPopupObject.SetActive(false);
        });
    }
}
