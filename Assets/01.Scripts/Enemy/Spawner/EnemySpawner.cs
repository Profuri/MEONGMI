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
        
        var phaseInfoList = PhaseManager.Instance.PhaseInfoList;
        
        EnemyListSO enemyList = phaseInfoList[phase].enemyListSO;
        int appearMaxEnemyCnt = phaseInfoList[phase].appearMaxEnemyCnt;
        int appearDelay = phaseInfoList[phase].appearDelay;
        int appearMaxOnceEnemyCnt = phaseInfoList[phase].appearOnceMaxEnemyCnt;
        int appearMinOnceEnemyCnt = phaseInfoList[phase].appearOnceMinEnemyCnt;
        int randomAppearEnemyCnt;
        
        _currentEnemyList.Clear();
        
        
        while (appearMaxEnemyCnt != _currentDeadCnt)
        {
            if (appearMaxEnemyCnt > _currentEnemyList.Count)
            {
                randomAppearEnemyCnt = Random.Range(appearMinOnceEnemyCnt,appearMaxOnceEnemyCnt);
                Vector3 randomPos;
                float lineLength = GameManager.Instance.PlayerController.LineConnect.LineLength;
                
                for (int i = 0; i < randomAppearEnemyCnt; i++)
                {
                    var spherePoint = Random.insideUnitSphere;
                    spherePoint.y = 0;
                    var dir = spherePoint.normalized;
                    var randomPoint = dir * Random.Range(lineLength, GameManager.Instance.MaxDistance);
                    var unitPoint = randomPoint + GameManager.Instance.BaseTrm.position;
                    unitPoint.y = 100f;
                    
                    bool result = Physics.Raycast(unitPoint,Vector3.down,out RaycastHit hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground"));
                    
                    if (result)
                    {
                        unitPoint.y = hitInfo.point.y;
                    }
                                        
                    var prefab = enemyList.GetRandomEnemy();
                
                    BaseEnemy enemy = PoolManager.Instance.Pop(prefab.name) as BaseEnemy;
                
                    enemy.Init();
                    enemy.gameObject.SetActive(true);
                    enemy.SetPosition(unitPoint);
                    _currentEnemyList.Add(enemy);
                    

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
