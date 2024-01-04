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

    private UnitType _currentUnit;
    [SerializeField] private SpriteRenderer _miniMapLine;

    private void Awake()
    {
        Collider = GetComponent<Collider>();
        _arc = transform.Find("Arc").GetComponent<Arc>();
        ResManager.Instance.OnResourceToZero += () => Destroy(this.gameObject);
        curUnitCount = 0;

        PhaseManager.Instance.OnPhaseChange += HandlePhaseChange;
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
        if (ResManager.Instance.PlayerResCnt <= 0)
        {
            return;
        }
        
        entity.StateMachine.ChangeState(PlayerStateType.Idle);
        ResManager.Instance.MoveResource();
        // TestUIManager.Instance.UpgradeRootPanelOn();
        
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

    public void SetMiniMapLine(float length)
    {
        _miniMapLine.transform.localScale = Vector3.one * (length + 5f);
    }
        
    public void AddUnit()
    {
        curUnitCount++;
    }

    private void HandlePhaseChange(PhaseType phase)
    {
        switch (phase)
        {
            case PhaseType.Rest:
                ChangeUnit(UnitType.Miner, UnitManager.Instance.UnitCountDictionary[UnitType.Miner]);
                break;
            case PhaseType.Raid:
                ChangeUnit(UnitType.Attacker, UnitManager.Instance.UnitCountDictionary[UnitType.Attacker]);
                break;
        }
    }

    private void ChangeUnit(UnitType type, int count)
    {
        foreach (var unit in UnitManager.Instance.UnitList)
        {
            UnitManager.Instance.DeleteUnit(unit);
        }

        for (int i = 0; i < count; i++)
        {
            CreateUnit(type);
        }

        _currentUnit = type;
    }
}
