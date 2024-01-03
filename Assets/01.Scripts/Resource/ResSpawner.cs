using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResSpawner
{
    private List<PhaseInfoSO> _phaseInfoList;
    private bool _isOn;
    
    public ResSpawner(List<PhaseInfoSO> phaseInfoList)
    {
        this._phaseInfoList = phaseInfoList;
        _isOn = true;
        ResManager.Instance.StartCoroutine(SpawnResourceCor());
    }

    private IEnumerator SpawnResourceCor()
    {
        while (true)
        {
            int currentPhase = PhaseManager.Instance.Phase;

            int spawnCnt = _phaseInfoList[currentPhase].GetSpawnCnt();
            float delaySpawnTime = _phaseInfoList[currentPhase].GetSpawnTime();

            Vector3 baseTrmPos = GameManager.Instance.BaseTrm.position;
            
            for (int i = 0; i < spawnCnt; i++)
            {
                //멀어진거에 따라 개수가 다르게 증가
                Vector3 randomPos = Random.insideUnitSphere * GameManager.Instance.MaxDistance + baseTrmPos;
                randomPos.y = 100f;
                bool result = Physics.Raycast(randomPos,Vector3.down,out RaycastHit info,Mathf.Infinity,1 << LayerMask.NameToLayer("Ground"));
                if (result)
                {
                    randomPos.y = info.point.y;
                }
                else
                {
                    Debug.LogError("Can't check ground !!!");
                }
                
                var resCnt = (int)(Vector3.Distance(randomPos,baseTrmPos) / 10f * 100f);
                //제곱을 해
                ResourceMono resource = PoolManager.Instance.Pop("ResourceMono") as ResourceMono;
                resource.SetScale(resCnt / 100f);
                resource.SetResourceCnt(resCnt);
                resource.transform.position = randomPos;
                yield return null;
            }
            yield return new WaitForSeconds(delaySpawnTime);
        }
    }
    
    
    
    
    
    

    
}
