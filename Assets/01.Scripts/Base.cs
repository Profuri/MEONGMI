using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Base : Interactable, IDamageable
{
    private int curUnitCount;
    public int CurUnitCount
    {
        get { return curUnitCount; }
        private set { Mathf.Clamp(curUnitCount, 0, StatManager.Instance.UnitMaxValue); }
    }

    public Collider Collider { get; private set; }
    private Arc _arc;
    
    private void Awake()
    {
        Collider = GetComponent<Collider>();
        _arc = transform.Find("Arc").GetComponent<Arc>();
        ResManager.Instance.OnResourceToZero += () => Destroy(this.gameObject);
        curUnitCount = 0;
    }
    
    public void Damaged(float damage)
    {
        //이펙트 소환.
        VFXPlayer vfxPlayer = PoolManager.Instance.Pop("ResHitParticle") as VFXPlayer;
        Vector3 offset = Vector3.up * 0.75f;
        vfxPlayer.transform.position = _arc.transform.position + offset;
        vfxPlayer.PlayEffect();
        _arc.ShakePosition();
        
        CameraManager.Instance.ImpulseCam(1, 0.1f, new Vector3(0, -1, 0));
        ResManager.Instance.UseResource((int)damage);
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


    public override void OnInteract(Entity entity)
    {
        entity.StateMachine.ChangeState(PlayerStateType.Idle);
        ResManager.Instance.MoveResource();
        TestUIManager.Instance.UpgradeRootPanelOn();
        
        PlayerController playerController = entity as PlayerController;
        if (playerController != null)
        {
            LineRenderer line = playerController.LineConnect.Line.LineRenderer;
            if (line != null)
            {
                ResourceParticle resParticle = PoolManager.Instance.Pop("ResParticle") as ResourceParticle;
                resParticle.Init();
                resParticle.ChaseLine(line);
                
            }
        }
    }
        
    public void AddUnit()
    {
        curUnitCount++;
    }
}
