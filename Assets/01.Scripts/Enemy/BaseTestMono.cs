using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BaseTestMono : MonoBehaviour
{
    [SerializeField] private EntityStatSO _entityStatSO;
    private float _hp;

    private void Awake()
    {
        _hp = _entityStatSO.maxHp;
    }

    public void Damaged(float damage)
    {
        VFXPlayer vfxPlayer = PoolManager.Instance.Pop("ResHitParticle") as VFXPlayer;
        Vector3 offset = Vector3.up * 1.5f;
        vfxPlayer.transform.position = transform.position + offset;
        vfxPlayer.PlayEffect();
        
        _hp -= damage;
    }

    public void CreateUnit(UnitType type)
    {
        Vector3 playerPos = transform.position - GameManager.Instance.PlayerTrm.position;
        playerPos.y = 0f;
        BaseUnit unit = UnitManager.Instance.CreateUnit(type, playerPos);
        unit.transform.SetParent(transform);
        unit.SetPosition(playerPos + transform.position);
        unit.HoldPosition = playerPos + transform.position;
        unit.LineConnect.SetLine(CreateLine(unit));
        unit.LineConnect.SetBaseConnectHole(transform.Find("Arc"));
        unit.LineConnect.SetLenght(unit.UnitStatSO.holdRange);
        unit.LineConnect.Connect();
    }

    private Line CreateLine(BaseUnit unit)
    {
        Line line = PoolManager.Instance.Pop("Line") as Line;
        line.transform.SetParent(transform.Find("Arc"));
        line.transform.localPosition = Vector3.zero;

        LaserHolder holder = PoolManager.Instance.Pop("LaserHolder") as LaserHolder;
        holder.transform.SetParent(null);
        line.GetComponent<CableProceduralSimple>().SetEndPoint(holder.transform);
        unit.LineConnect.SetConnectHolder(holder);
        return line;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateUnit(UnitType.Miner);
        }
    }
}
