using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI timerUI;
    [SerializeField] private Button oneMoreButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TMP_Text timerText;

    private void OnEnable()
    {
        BackgroundMusic.Instance.PlayBackgroundMusic(false);
        AudioManager.Instance.Play(SoundType.WinSound);
        timerText.text = timerUI.GetFinalTime();
        oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);
        mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            SceneManager.LoadScene(Consts.SceneNames.MenuScene);
        });
    }

    private void OnOneMoreButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound);
        SceneManager.LoadScene(Consts.SceneNames.GameScene);
    }
}
