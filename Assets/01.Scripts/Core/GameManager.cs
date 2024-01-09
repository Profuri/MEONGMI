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

    [SerializeField] private PoolingListSO _poolingList;
    
    [SerializeField] 
    private float _maxDistance;
    public float MaxDistance => _maxDistance;

    private void Awake()
    {
        SceneManagement.Instance.OnRestartGameEvent += () =>
        {
            Destroy(this.gameObject);
        };
        
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance.Init();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public override void Init()
    {

        SceneManagement.Instance.Init();
        PoolManager.Instance = new PoolManager(transform);
        foreach (var pair in _poolingList.pairs)
        {
            PoolManager.Instance.CreatePool(pair.prefab, pair.count);
        }

        SceneManagement.Instance.OnTutorialStartEvent += () =>
        {
            CameraManager.Instance.Init();
        };
        
        SceneManagement.Instance.OnGameStartEvent += () =>
        {
            CameraManager.Instance.Init();
            PhaseManager.Instance.Init();
            EnemySpawner.Instance.Init();

            PhaseManager.Instance.Init();
        
            ResManager.Instance.Init();
            
            UnitManager.Instance.Init();

            UIManager.Instance.Init();


            SoundManager.Instance.PlayBGM("Rehearsal Decay");
        };
    }
}
