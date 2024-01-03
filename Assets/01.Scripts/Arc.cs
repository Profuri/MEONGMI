using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arc : MonoBehaviour
{
    private List<Transform> _arcList = new List<Transform>();
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private float _duration;
    [SerializeField] private float _strength;
    [SerializeField] private int _vibrato = 1;
    [SerializeField] private float _randomness;
    [SerializeField] private bool _fadeOut;

    private Vector3 _originPos;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _arcList.Add(transform.GetChild(i));
        }
        
        // 랜덤한 주기, 진폭, 위상을 설정합니다.
        frequency = Random.Range(1f, 5f) * 0.5f;  // 주기
        amplitude = Random.Range(1f, 5f) * 0.5f;   // 진폭
        phaseOffset = Random.Range(0f, 2f * Mathf.PI) * 0.1f;  // 위상

        _originPos = transform.position;
    }
    private float frequency;
    private float amplitude;
    private float phaseOffset;

    private Tween _tween;
    public float SineFunction(float time)
    {
        // 불규칙한 사인 함수
        return amplitude * Mathf.Sin(2 * Mathf.PI * frequency * time + phaseOffset);
    }

    public float CosineFunction(float time)
    {
        // 불규칙한 코사인 함수
        return amplitude * Mathf.Cos(2 * Mathf.PI * frequency * time + phaseOffset);
    }

    public float TangentFunction(float time)
    {
        // 불규칙한 탄젠트 함수 (주기가 짧을 때 큰 값이 나올 수 있습니다)
        return amplitude * Mathf.Tan(2 * Mathf.PI * frequency * time + phaseOffset);
    }
    private void Update()
    {

        transform.Rotate(new Vector3(Mathf.Sin((Time.time)),Mathf.Sin(Time.time),Mathf.Sin(Time.time)));
        
        for(int i= 0 ; i < _arcList.Count; i++)
        {
            Transform trm = _arcList[i];
            int direction = i % 2 == 0 ? 1 : -1;
            trm.Rotate(new Vector3(0,_rotateSpeed * direction * Time.deltaTime ,0));
        }
    }

    public void ShakePosition()
    {
        if (_tween != null)
        {
            // _tween.Kill();
            DOTween.Kill(_tween);
            transform.position = _originPos;
        }
        _tween = transform.DOShakePosition(_duration,_strength,_vibrato,_randomness,_fadeOut);

    }
}
