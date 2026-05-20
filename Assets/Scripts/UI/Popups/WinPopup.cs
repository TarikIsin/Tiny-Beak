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
        timerText.text = timerUI.GetFinalTime();
        oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);
    }

    private void OnOneMoreButtonClicked()
    {
        SceneManager.LoadScene(Consts.SceneNames.GameScene);
    }
}
