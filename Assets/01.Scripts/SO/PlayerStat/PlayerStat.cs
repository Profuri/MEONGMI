using System;
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
    private Entity _owner;

    public void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    private void IncreaseStatBy(int modifyValue, float duration, PlayerStatType type)
    {
        _owner.StartCoroutine(StatModifyRoutine(modifyValue, duration, type));
    }

    private IEnumerator StatModifyRoutine(int modifyValue, float duration, PlayerStatType type)
    {
        var stat = GetStatByType(type);
        stat.AddModifier(modifyValue);
        yield return new WaitForSeconds(duration);
        stat.RemoveModifier(modifyValue);
    }

    private void OnEnable()
    {
        _fieldInfoDictionary ??= new Dictionary<PlayerStatType, FieldInfo>();
        _fieldInfoDictionary.Clear();

        var statType = typeof(PlayerStat);
        foreach (PlayerStatType type in Enum.GetValues(typeof(PlayerStatType)))
        {
            var statField = statType.GetField(type.ToString());
            if (statField == null)
            {
                Debug.LogError($"There are no stat : {type.ToString()}");
            }
            else
            {
                _fieldInfoDictionary.Add(type, statField);
            }
        }
    }

    public Stat GetStatByType(PlayerStatType type)
    {
        return _fieldInfoDictionary[type].GetValue(this) as Stat;
    }
}
