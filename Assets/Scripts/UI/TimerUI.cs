using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform timerRotatableTransform;
    [SerializeField] private TMP_Text timerText;

    [Header("Settings")]
    [SerializeField] private float rotationDuration;
    [SerializeField] private Ease rotationEase;

    private float elapsedTime;
    private bool isTimerRunning;
    private Tween rotationTween;
    private string finalTime;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Play:
                StartTimer();
                break;
            case GameState.Pause:
                StopTimer();
                break;
            case GameState.Resume:
                ResumeTimer();
                break;
            case GameState.GameOver:
                FinishTimer();
                break;
        }
    }

    private void StartTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true;
        UpdateTimerText();
        PlayRotationAnimation();
        InvokeRepeating(nameof(Tick), 1f, 1f);
    }

    private void StopTimer()
    {
        isTimerRunning = false;
        CancelInvoke(nameof(Tick));
        rotationTween?.Pause();
    }

    private void ResumeTimer()
    {
        if (isTimerRunning) return;
        isTimerRunning = true;
        rotationTween?.Play();
        InvokeRepeating(nameof(Tick), 1f, 1f);
    }

    private void FinishTimer()
    {
        finalTime = GetFormattedTime(elapsedTime); 
        StopTimer();
    }

    private void Tick()
    {
        if (!isTimerRunning) return;
        elapsedTime += 1f;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        timerText.text = GetFormattedTime(elapsedTime);
    }

    private void PlayRotationAnimation()
    {
        rotationTween?.Kill();
        rotationTween = timerRotatableTransform
            .DORotate(new Vector3(0f, 0f, -360f), rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(rotationEase);
    }

    private string GetFormattedTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public string GetFinalTime()
    {
        return finalTime;
    }
}