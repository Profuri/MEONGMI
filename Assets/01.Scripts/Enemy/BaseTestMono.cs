using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BaseTestMono : MonoBehaviour,IDamageable
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
        BaseUnit unit = UnitManager.Instance.CreateUnit(type);
        unit.transform.SetParent(transform);
        Vector3 playerPos = GameManager.Instance.PlayerTrm.position;
        unit.SetPosition(new Vector3(-playerPos.x, playerPos.y, -playerPos.z));
        if(type == UnitType.Attacker)
        {
            AttackerUnit atk = unit as AttackerUnit;
            atk.HoldPosition = new Vector3(-playerPos.x, playerPos.y, -playerPos.z);
        }
        else if(type == UnitType.Miner)
        {
            MinerUnit miner = unit as MinerUnit;
        }
        unit.LineConnect.SetLine(CreateLine());
        unit.LineConnect.SetBaseConnectHole(transform.Find("Arc"));
        unit.LineConnect.SetLenght(unit.UnitStatSO.holdRange);
        unit.LineConnect.Connect();
    }

    private Line CreateLine()
    {
        Line line = PoolManager.Instance.Pop("Line") as Line;
        return line;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateUnit(UnitType.Attacker);
        }
    }
}
