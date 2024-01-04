using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitLineConnect : MonoBehaviour
{
    [SerializeField] private Transform _unitRoot;
    [SerializeField] private Transform _unitConnectHolder;
    [SerializeField] private Transform _unitConnectHole;
    [SerializeField] private Transform _baseConnectHole;

    [SerializeField] private Line _line;

    [SerializeField] private float _lineLength;
    public float LineLength => _lineLength;

    private Vector3 _lastConnectPos;

    private BaseUnit _unit;

    private bool _connect;

    private void Awake()
    {
        _unit = GetComponent<BaseUnit>();
    }

    //private void Start()
    //{
    //    _line.SetStartHole(_baseConnectHole);
    //    _line.SetEndHole(_unitConnectHolder);

    //    ConnectLine();
    //}

    private void Update()
    {
        if (_connect)
        {
            _lastConnectPos = _unitConnectHole.position;
            _line.LineUpdate();
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
        _line.SetStartHole(_baseConnectHole);
        _line.SetEndHole(_unitConnectHolder);

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

    public void SetLine(Line line)
    {
        _line = line;
    }

    public void SetBaseConnectHole(Transform trm)
    {
        _baseConnectHole = trm;
    }

    public void Connect()
    {
        _line.SetStartHole(_baseConnectHole);
        _line.SetEndHole(_unitConnectHolder);
        ConnectLine();
    }

    public void SetConnectHolder(LaserHolder holder)
    {
        _unitConnectHolder = holder.transform;
    }

    public void Delete()
    {
        PoolManager.Instance.Push(_unitConnectHolder.GetComponent<LaserHolder>());
        PoolManager.Instance.Push(_line);
    }
}