using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum PlayerStatType
{
    health,
    armor,
    damage,
    recovery,
    luck,
    shotCnt,
    shotSpeed,
    moveSpeed,
    gatheringSpeed,
}

[CreateAssetMenu(menuName = "SO/Stat/Player")]
public class PlayerStat : ScriptableObject
{
    public Stat health;
    public Stat armor;
    public Stat damage;
    public Stat recovery;
    public Stat luck;
    public Stat shotCnt;
    public Stat shotSpeed;
    public Stat moveSpeed;
    public Stat gatheringSpeed;

    private Dictionary<PlayerStatType, FieldInfo> _fieldInfoDictionary;
    // private Entity _owner;

    // public void SetOwner(Entity owner)
    // {
        // _owner = owner;
    // }

    private IEnumerator IncreaseStatBy(int modifyValue, float duration, PlayerStatType type)
    {
        
    }
}
