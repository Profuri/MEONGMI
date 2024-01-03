using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform PlayerTrm => PlayerController.transform;
    private PlayerController _playerController;
    public PlayerController PlayerController
    {
        get
        {
            if (_playerController is null)
            {
                _playerController = FindObjectOfType<PlayerController>();
            }
            return _playerController;
        }
    }

    public Transform BaseTrm => Base.transform;
    private Base _base;
    public Base Base
    {
        get
        {
            if (_base == null)
            {
                _base = FindObjectOfType<Base>();
            }
            return _base;
        }
    }
    public Camera MainCam { get; private set; }

    [SerializeField] private PoolingListSO _poolingList;
    [SerializeField] 
    private float _maxDistance;
    public float MaxDistance => _maxDistance;

    private void Awake()
    {
        GameManager.Instance.Init();
    }

    public override void Init()
    {
        MainCam = Camera.main;
        PoolManager.Instance = new PoolManager(transform);
        foreach (var pair in _poolingList.pairs)
        {
            PoolManager.Instance.CreatePool(pair.prefab, pair.count);
        }
        
        PhaseManager.Instance.Init();
        EnemySpawner.Instance.Init();
        EnemySpawner.Instance.StartPhase(PhaseManager.Instance.Phase);
        
        ResManager.Instance.Init();
        //UIManager.Instance.Init();
    }
}
