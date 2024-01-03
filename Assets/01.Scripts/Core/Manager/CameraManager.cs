using System.Collections;
using System.Drawing;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineVirtualCamera _playerFollowCam;

    private CinemachineImpulseSource _impulseSource;
    private Camera _mainCam;

    private Coroutine _runningRoutine = null;
    
    public override void Init()
    {
        _playerFollowCam = FindObjectOfType<CinemachineVirtualCamera>();
        _mainCam = Camera.main;
        _impulseSource = _playerFollowCam.GetComponent<CinemachineImpulseSource>();
    }

    public void ImpulseCam(float intensity, float time, Vector3 velocity)
    {
        _impulseSource.m_DefaultVelocity = velocity;
        _impulseSource.GenerateImpulse(intensity);
        _impulseSource.m_ImpulseDefinition.m_ImpulseDuration = time;
    }

    public void Zoom(float size, float time)
    {
        if (_runningRoutine != null)
        {
            StopCoroutine(_runningRoutine);
        }
        _runningRoutine = StartCoroutine(ZoomRoutine(size, time));
    }

    private IEnumerator ZoomRoutine(float size, float time)
    {
        var current = 0f;
        var prevSize = _playerFollowCam.m_Lens.OrthographicSize;

        while (current <= time)
        {
            current += Time.deltaTime;
            var percent = current / time;
            var curSize = Mathf.Lerp(prevSize, size, percent);
            _playerFollowCam.m_Lens.OrthographicSize = curSize;
            yield return null;
        }
        
        _playerFollowCam.m_Lens.OrthographicSize = size;
    }
}