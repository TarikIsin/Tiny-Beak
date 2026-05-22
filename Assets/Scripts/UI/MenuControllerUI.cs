using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            SceneManager.LoadScene(Consts.SceneNames.GameScene);
        } );

        quitButton.onClick.AddListener(() => 
        {
            AudioManager.Instance.Play(SoundType.ButtonClickSound);
            Debug.Log("Quiting the Game!");
            Application.Quit();
        } );
    }
}
