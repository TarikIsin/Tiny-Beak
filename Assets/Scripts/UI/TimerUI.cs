using DG.Tweening;
using System;
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

    private void Start()
    {
        PlayRotationAnimation();
        StartTimer();
    }

    private void PlayRotationAnimation()
    {
        timerRotatableTransform.DORotate(new Vector3(0f, 0f, -360f), rotationDuration,
            RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(rotationEase);
    }

    private void StartTimer()
    {
        elapsedTime = 0f;
        InvokeRepeating(nameof(UpdateTimeUI), 0f, 1f);
    }

    private void UpdateTimeUI()
    {
        elapsedTime += 1f;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
