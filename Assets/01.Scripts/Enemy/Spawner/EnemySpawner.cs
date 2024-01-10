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

    [SerializeField] private LayerMask _obstacleLayer;

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
        
        EnemyListSO enemyList = phaseInfoList[phase - 1].enemyListSO;
        _appearMaxEnemyCnt = phaseInfoList[phase - 1].appearMaxEnemyCnt;
        int appearDelay = phaseInfoList[phase - 1].appearDelay;
        int appearMaxOnceEnemyCnt = phaseInfoList[phase - 1].appearOnceMaxEnemyCnt;
        int appearMinOnceEnemyCnt = phaseInfoList[phase - 1].appearOnceMinEnemyCnt;
        
        OnEnemyDead?.Invoke(RemainMonsterCnt);
        
        _currentEnemyList.Clear();

        while (RemainMonsterCnt > 0)
        {
            if (_appearMaxEnemyCnt > _currentEnemyList.Count)
            {
                int randomAppearEnemyCnt = Random.Range(appearMinOnceEnemyCnt,appearMaxOnceEnemyCnt);

                if (randomAppearEnemyCnt > _appearMaxEnemyCnt - _currentEnemyList.Count)
                {
                    randomAppearEnemyCnt = _appearMaxEnemyCnt - _currentEnemyList.Count;
                }
                
                float lineLength = GameManager.Instance.PlayerController.LineConnect.LineLength;

                Func<Vector3> getRandomPos = () =>
                {
                    Vector3 spherePoint = Random.insideUnitSphere;
                    spherePoint.y = 0;
                    Vector3 dir = spherePoint.normalized;
                    Vector3 randomPoint = dir * Random.Range(lineLength, GameManager.Instance.MaxDistance);
                    Vector3 unitPoint = randomPoint + GameManager.Instance.BaseTrm.position;
                    unitPoint.y = 100f;
                    return unitPoint;
                };

                int enemyCnt = 0;
                while (enemyCnt < randomAppearEnemyCnt)
                {
                    Vector3 unitPoint = getRandomPos();
                    bool result = Physics.Raycast(unitPoint, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, _obstacleLayer);

                    if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground") && result)
                    {
                        unitPoint.y = hitInfo.point.y;
                    }
                    else
                    {
                        Debug.LogError($"Can't place this position: {unitPoint}");
                        continue;
                    }

                    var prefab = enemyList.GetRandomEnemy();

                    BaseEnemy enemy = PoolManager.Instance.Pop(prefab.name) as BaseEnemy;

                    enemy.Init();
                    enemy.gameObject.SetActive(true);
                    enemy.SetPosition(unitPoint);
                    _currentEnemyList.Add(enemy);

                    enemyCnt++;
                    yield return null;
                }
                yield return new WaitForSeconds(appearDelay);
            }
            yield return null;
        }
        
        PhaseManager.Instance.ChangePhase(PhaseType.Rest);
    }

    public void DeadEnemy(BaseEnemy enemy)
    {
        int phase = PhaseManager.Instance.Phase;
        int randomEnemyResCnt = PhaseManager.Instance.PhaseInfoList[phase].GetEnemyRandomResCnt();
        
        //DropResource dropResource = PoolManager.Instance.Pop("DropResource") as DropResource;
        //dropResource.Init();
        //dropResource.SetResourceAmount(randomEnemyResCnt);
        //dropResource.transform.position = enemy.transform.position;
        
        PoolManager.Instance.Push(enemy);
        _currentDeadCnt++;
        OnEnemyDead?.Invoke(RemainMonsterCnt);
    }
}
