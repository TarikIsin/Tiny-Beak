using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI timerUI;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TMP_Text timerText;


    private void OnEnable()
    {
        BackgroundMusic.Instance.PlayBackgroundMusic(false);
        AudioManager.Instance.Play(SoundType.LoseSound);
        timerText.text = timerUI.GetFinalTime();
        tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            SceneManager.LoadScene(Consts.SceneNames.MenuScene);
        });
    }

    private void OnTryAgainButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound);
        SceneManager.LoadScene(Consts.SceneNames.GameScene);
    }
}
