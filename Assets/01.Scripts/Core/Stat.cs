using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float _baseValue;

    public List<float> modifiers = new List<float>();

    public float GetValue()
    {
        return _baseValue + modifiers.Sum();
    }

    public void SetDefaultValue(float value)
    {
        _baseValue = value;
    }

    public void AddModifier(float value)
    {
        if (value != 0)
        {
            modifiers.Add(value);
        }
    }

    public void RemoveModifier(float value)
    {
        if (value != 0)
        {
            modifiers.Remove(value);
        }
    }
}