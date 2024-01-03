using System;
using UnityEngine;

public class UnitLineConnect : MonoBehaviour
{
    [SerializeField] private Transform _unitRoot;
    [SerializeField] private Transform _unitConnectHole;
    [SerializeField] private Transform _baseConnectHole;

    [SerializeField] private Line _line;

    [SerializeField] private float _lineLenght;

    public void SetLine(Line line)
    {
        _line = line;
        if(_line != null)
        {
            _line.SetStartHole(_baseConnectHole);
            _line.SetEndHole(_unitConnectHole);
        }
    }

    private void LateUpdate()
    {
        LerpPositionInCircle();
    }

    private void LerpPositionInCircle()
    {
        var vec = _unitRoot.position - _baseConnectHole.position;
        var yAxis = vec.y;
        vec = Vector3.ClampMagnitude(vec, _lineLenght);
        vec.y = yAxis;
        _unitRoot.position = _baseConnectHole.position + vec;
    }

    public void SetLenght(float lenght)
    {
        _lineLenght = lenght;
    }
}