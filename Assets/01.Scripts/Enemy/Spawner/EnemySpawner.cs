using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    private List<BaseEnemy> _currentEnemyList;
    private int _currentDeadCnt;
    
    private Coroutine _phaseCoroutine;
    public event Action<int> OnPhaseEnd;

    public int RemainMonsterCnt => _appearMaxEnemyCnt - _currentDeadCnt;
    private int _appearMaxEnemyCnt;
    
    public event Action<int> OnEnemyDead; 

    public override void Init()
    {
        _currentEnemyList = new List<BaseEnemy>();
        _currentDeadCnt = 0;
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
        _appearMaxEnemyCnt = phaseInfoList[phase].appearMaxEnemyCnt;
        int appearDelay = phaseInfoList[phase].appearDelay;
        int appearMaxOnceEnemyCnt = phaseInfoList[phase].appearOnceMaxEnemyCnt;
        int appearMinOnceEnemyCnt = phaseInfoList[phase].appearOnceMinEnemyCnt;
        int randomAppearEnemyCnt;

        //RemainEnemyCnt = appearMaxEnemyCnt;
        //OnEnemyDead?.Invoke(RemainEnemyCnt);
        
        _currentEnemyList.Clear();

        while (_appearMaxEnemyCnt > _currentDeadCnt)
        {
            if (_appearMaxEnemyCnt > _currentEnemyList.Count)
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
        
        PhaseManager.Instance.ChangePhase(PhaseType.Rest);
    }

    public void DeadEnemy(BaseEnemy enemy)
    {
        int phase = PhaseManager.Instance.Phase;
        int randomEnemyResCnt = PhaseManager.Instance.PhaseInfoList[phase].GetEnemyRandomResCnt();
        
        DropResource dropResource = PoolManager.Instance.Pop("DropResource") as DropResource;
        dropResource.Init();
        dropResource.SetResourceAmount(randomEnemyResCnt);
        dropResource.transform.position = enemy.transform.position;
            
        PoolManager.Instance.Push(enemy);
        _currentDeadCnt++;
        //RemainEnemyCnt--;
        //OnEnemyDead?.Invoke(RemainEnemyCnt);
    }
}
