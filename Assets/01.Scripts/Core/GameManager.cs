using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform PlayerTrm { get; private set; }
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
    public Transform BaseTrm { get; private set; }
    
    public Camera MainCam { get; private set; }

    [SerializeField] private PoolingListSO _poolingList;
    
    private void Awake()
    {
        GameManager.Instance.Init();
    }

    public override void Init()
    {
        MainCam = Camera.main;
        PlayerTrm = GameObject.Find("Player")?.transform;
        BaseTrm = GameObject.Find("Base")?.transform;
        
        PoolManager.Instance = new PoolManager(transform);
        foreach (var pair in _poolingList.pairs)
        {
            PoolManager.Instance.CreatePool(pair.prefab, pair.count);
        }

        EnemySpawner.Instance.Init();
        EnemySpawner.Instance.StartPhase(0);
        
        ResManager.Instance.Init();
        //UIManager.Instance.Init();
    }
}
