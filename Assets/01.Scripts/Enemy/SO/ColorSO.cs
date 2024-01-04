using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Color")]
public class ColorSO : ScriptableObject
{
    public List<ColorType> colorTypeList = new List<ColorType>();
    
    public Color GetColorByBulletType(BulletType type)
    {
        foreach (ColorType ct in colorTypeList)
        {
            if (ct.type == type)
            {
                return ct.color;
            }
        }
        Debug.LogError("Can't Find Color");
        return Color.blue;
    }
}
