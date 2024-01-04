using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLineConnect : MonoBehaviour
{
    [SerializeField] private Transform _playerRoot;
    [SerializeField] private Transform _playerConnectHolder;
    [SerializeField] private Transform _playerConnectHole;
    [SerializeField] private Transform _baseConnectHole;

    [SerializeField] private Line _line;
    public Line Line => _line;

    [SerializeField] private float _lineLength;
    public float LineLength => _lineLength;

    [SerializeField] private float _lineConnectToggleTime;
    [SerializeField] private float _lineControlCancelTime;

    [SerializeField] private float _detachableTime;
    private float _lastDetachTime;

    private Vector3 _lastConnectPos;
    
    private float _toggleTimer;
    private float _cancelTimer;

    private bool _updatingToggle;

    private PlayerController _playerController;

    private bool _connect;
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.InputReader.OnLineConnectEvent += ConnectHandler;
    }

    private void Start()
    {
        if (GameManager.Instance.Base != null)
        {
            GameManager.Instance.Base.SetMiniMapLine(_lineLength);
        }
    }

    public void Init()
    {
        _line.SetStartHole(_baseConnectHole);
        _line.SetEndHole(_playerConnectHolder);
        ConnectLine();
    }

    private void Update()
    {
        if (_updatingToggle)
        {
            if (Vector3.Distance(_lastConnectPos, _playerRoot.position) > 3f)
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
            _lastConnectPos = _playerConnectHole.position;
            _line.LineUpdate();
        }
        else
        {
            if (!_playerController.Dead)
            {
                if (Time.time > _lastDetachTime + _detachableTime)
                {
                    _playerController.Damaged(1000f);
                }
            }
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
            _playerConnectHolder.position = _playerConnectHole.position;
            LerpPositionInCircle();
        }
    }

    private void LerpPositionInCircle()
    {
        var vec = _playerRoot.position - _baseConnectHole.position;
        var yAxis = vec.y;
        vec = Vector3.ClampMagnitude(vec, _lineLength);
        vec.y = yAxis;
        _playerRoot.position = _baseConnectHole.position + vec;
    }

    private void ConnectHandler(bool updating)
    {
        _updatingToggle = updating;
    }

    private void ConnectLine()
    {
        _connect = true;
    }

    private void DetachLine()
    {
        _connect = false;
        _lastDetachTime = Time.time;
        _lastConnectPos = _playerConnectHole.position;
    }

    public void SetLenght(float lenght)
    {
        _lineLength = lenght;
        GameManager.Instance.Base.SetMiniMapLine(_lineLength);
    }
}