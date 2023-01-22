using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    
    private CinemachineVirtualCamera _virtualCamera;
    private float _shakeTimer;
    private float _shakeTimeTotal;
    private float _startingIntensity;

    private void Awake()
    {
        Instance = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        var basicMultiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        basicMultiChannelPerlin.m_AmplitudeGain = intensity;

        _startingIntensity = intensity;
        _shakeTimeTotal = time;
        _shakeTimer = time;
    }

    private void Update()
    {
        if (!(_shakeTimer > 0)) return;
        _shakeTimer -= Time.deltaTime; 
        // TIME OVER!
        var basicMultiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        basicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeTimeTotal));
    }
}