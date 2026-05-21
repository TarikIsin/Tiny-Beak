using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;

        m_MultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private IEnumerator CameraShakeCoroutine(float intensity, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        m_MultiChannelPerlin.AmplitudeGain = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
        startingIntensity = intensity;
    }

    public void ShakeCamera(float intensity, float time, float delay = 0f)
    {
        StartCoroutine(CameraShakeCoroutine(intensity, time, delay));
    }

    private void Update()
    {
        if (shakeTimer > 0f) 
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f) 
            {
                m_MultiChannelPerlin.AmplitudeGain = Mathf.Lerp(startingIntensity,
                    0f, 1 - (shakeTimer / shakeTimerTotal));
            }
        }
    }

}
