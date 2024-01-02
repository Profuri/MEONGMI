using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;



public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    [SerializeField] private List<PhaseInfoSO> _phaseInfoList = new List<PhaseInfoSO>();

    [SerializeField] private float _maxDistance;
    private List<BaseEnemy> _currentEnemyList;
    private int _currentDeadCnt;

    private Coroutine _phaseCoroutine;
    public event Action<int> OnPhaseEnd;
    
    public override void Init()
    {
        _currentEnemyList = new List<BaseEnemy>();
        _currentDeadCnt = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T");
            foreach (var enemy in _currentEnemyList)
            {
                Debug.Log("dd");
                enemy.Damaged(10);
            }
        }
    }
    
    public void StartPhase(int phase)
    {
        if (_phaseCoroutine != null)
        {
            StopCoroutine(_phaseCoroutine);
        }
        _phaseCoroutine = StartCoroutine(StartPhaseCor(phase));
    }
    
    
    private IEnumerator StartPhaseCor(int phase)
    {
        _currentDeadCnt = 0;
        
        EnemyListSO enemyList = _phaseInfoList[phase].enemyListSO;
        int appearMaxEnemyCnt = _phaseInfoList[phase].appearMaxEnemyCnt;
        int appearDelay = _phaseInfoList[phase].appearDelay;
        int appearMaxOnceEnemyCnt = _phaseInfoList[phase].appearOnceMaxEnemyCnt;
        int appearMinOnceEnemyCnt = _phaseInfoList[phase].appearOnceMinEnemyCnt;
        int randomAppearEnemyCnt;
        
        _currentEnemyList.Clear();
        
        
        while (appearMaxEnemyCnt != _currentDeadCnt)
        {
            if (appearMaxEnemyCnt > _currentEnemyList.Count)
            {
                //이거 수정해줘야됨.
                randomAppearEnemyCnt = Random.Range(appearMinOnceEnemyCnt,appearMaxOnceEnemyCnt);
                Vector3 randomPos;
                float lineLength = GameManager.Instance.PlayerController.LineConnect.LineLength;
                
                for (int i = 0; i < randomAppearEnemyCnt; i++)
                {
                    // Vector3 unitSphere = Random.insideUnitSphere * _maxDistance;
                    // unitSphere = new Vector3(clampValue(unitSphere.x),clampValue(unitSphere.y),clampValue(unitSphere.z));
                    //
                    // randomPos = unitSphere + GameManager.Instance.BaseTrm.position;
                    // randomPos.y = 100f;

                    var spherePoint = Random.insideUnitSphere;
                    spherePoint.y = 0;
                    var dir = spherePoint.normalized;
                    var randomPoint = dir * Random.Range(lineLength, _maxDistance);
                    var unitPoint = randomPoint + GameManager.Instance.BaseTrm.position;
                    unitPoint.y = 100f;
                    
                    bool result = Physics.Raycast(unitPoint,Vector3.down,out RaycastHit hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground"));
                    
                    if (result)
                    {
                        unitPoint.y = hitInfo.point.y;
                    }
                    else
                    {
                        Debug.LogError("DDDDDDDDDDDDDDD");
                    }
                                        
                    var prefab = enemyList.GetRandomEnemy();
                
                    BaseEnemy enemy = PoolManager.Instance.Pop(prefab.name) as BaseEnemy;
                
                    enemy.Init();
                    enemy.gameObject.SetActive(true);
                    enemy.SetPosition(unitPoint);
                    _currentEnemyList.Add(enemy);
                    
                    Debug.Log($"EnemyPos: {enemy.transform.position}");

                    yield return null;
                }
            }
            yield return new WaitForSeconds(appearDelay);
        }
        Debug.Log("OnPhaseEnd");
        OnPhaseEnd?.Invoke(phase);
    }

    public void DeadEnemy(BaseEnemy enemy)
    {
        Debug.Log($"DeadEnemy: {enemy}");
        PoolManager.Instance.Push(enemy);
        _currentDeadCnt++;
    }
    //_enemyListSO.GetRandomEnemy();


}
