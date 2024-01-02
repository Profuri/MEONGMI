using System;
using UnityEngine;

public class PlayerLineConnect : MonoBehaviour
{
    [SerializeField] private Transform _playerRoot;
    [SerializeField] private Transform _playerConnectHole;
    [SerializeField] private Transform _baseConnectHole;

    [SerializeField] private Line _line;

    [SerializeField] private float _lineLenght;

    private void Awake()
    {
        _line.SetStartHole(_baseConnectHole);
        _line.SetEndHole(_playerConnectHole);
    }

    private void LateUpdate()
    {
        LerpPositionInCircle();
    }

    private void LerpPositionInCircle()
    {
        var vec = _playerRoot.position - _baseConnectHole.position;
        var yAxis = vec.y;
        vec = Vector3.ClampMagnitude(vec, _lineLenght);
        vec.y = yAxis;
        _playerRoot.position = _baseConnectHole.position + vec;
    }

    public void SetLenght(float lenght)
    {
        _lineLenght = lenght;
    }
}