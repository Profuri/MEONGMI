using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResSpawner
{
    public static int currentRes = 0;
    const int maxCnt = 8;
    private List<PhaseInfoSO> _phaseInfoList;
    private bool _isOn;
    
    public ResSpawner(List<PhaseInfoSO> phaseInfoList)
    {
        currentRes = 0;
        this._phaseInfoList = phaseInfoList;
        _isOn = true;
        ResManager.Instance.StartCoroutine(SpawnResourceCor());
    }

    private IEnumerator SpawnResourceCor()
    {
        while (true )
        {
            yield return new WaitUntil(()=> currentRes <= maxCnt && PhaseManager.Instance.PhaseType == PhaseType.Rest);
           
            int currentPhase = PhaseManager.Instance.Phase;

            int spawnCnt =_phaseInfoList[currentPhase].GetSpawnCnt();
            if (spawnCnt + currentRes > maxCnt)
                spawnCnt = maxCnt - currentRes;
            float delaySpawnTime = _phaseInfoList[currentPhase].GetSpawnTime();

            Vector3 baseTrmPos = GameManager.Instance.BaseTrm.position;
            
            for (int i = 0; i < spawnCnt; i++)
            {
                //멀어진거에 따라 개수가 다르게 증가
                Vector3 randomPos = Random.insideUnitSphere * GameManager.Instance.PlayerController.LineConnect.LineLength;
                randomPos = (baseTrmPos + randomPos.normalized * 10) + randomPos;
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
                
                var resCnt = (int)(Vector3.Distance(randomPos,baseTrmPos) / 10f * 200f);
                //제곱을 해
                ResourceMono resource = PoolManager.Instance.Pop("ResourceMono") as ResourceMono;
                resource.SetScale(resCnt / 200f);
                resource.SetResourceCnt(resCnt);
                resource.transform.position = randomPos;
                currentRes++;
                yield return null;
            }
            yield return new WaitForSeconds(delaySpawnTime);
        }
    }
    
    
    
    
    
    

    
}
