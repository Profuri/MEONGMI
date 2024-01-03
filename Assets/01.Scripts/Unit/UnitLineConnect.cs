using System;
using UnityEngine;

public class UnitLineConnect : MonoBehaviour
{
    [SerializeField] private Transform _unitRoot;
    [SerializeField] private Transform _unitConnectHolder;
    [SerializeField] private Transform _unitConnectHole;
    [SerializeField] private Transform _baseConnectHole;

    [SerializeField] private Line _line;

    [SerializeField] private float _lineLength;
    public float LineLength => _lineLength;

    [SerializeField] private float _lineConnectToggleTime;
    [SerializeField] private float _lineControlCancelTime;

    private Vector3 _lastConnectPos;

    private float _toggleTimer;
    private float _cancelTimer;

    private bool _updatingToggle;

    private BaseUnit _unit;

    private bool _connect;

    private void Awake()
    {
        _unit = GetComponent<BaseUnit>();
    }

    private void Update()
    {
        if (_updatingToggle)
        {
            if (Vector3.Distance(_lastConnectPos, _unitRoot.position) > 3f)
            {
                return;
            }

            _toggleTimer += Time.deltaTime;
            _cancelTimer = 0f;
            if (_toggleTimer >= _lineConnectToggleTime)
            {
                if (_connect)
                {
                    DetachLine();
                }
                else
                {
                    ConnectLine();
                }

                _updatingToggle = false;
            }
        }

        if (_connect)
        {
            _lastConnectPos = _unitConnectHole.position;
            _line.LineUpdate();
        }

        if (_toggleTimer >= 0f)
        {
            _cancelTimer += Time.deltaTime;
            if (_cancelTimer >= _lineControlCancelTime)
            {
                _toggleTimer = 0f;
            }
        }
    }

    private void LateUpdate()
    {
        if (_connect)
        {
            _unitConnectHolder.position = _unitConnectHole.position;
            LerpPositionInCircle();
        }
    }

    private void LerpPositionInCircle()
    {
        var vec = _unitRoot.position - _baseConnectHole.position;
        var yAxis = vec.y;
        vec = Vector3.ClampMagnitude(vec, _lineLength);
        vec.y = yAxis;
        _unitRoot.position = _baseConnectHole.position + vec;
    }

    private void ConnectLine()
    {
        _connect = true;
    }

    private void DetachLine()
    {
        _connect = false;
        _lastConnectPos = _unitConnectHole.position;
    }

    public void SetLenght(float lenght)
    {
        _lineLength = lenght;
    }

    public void Init(Line line, Transform baseConnectHole)
    {
        _line = line;
        _baseConnectHole = baseConnectHole;
        _line.SetStartHole(_baseConnectHole);
        _line.SetEndHole(_unitConnectHolder);
        ConnectLine();
    }
}