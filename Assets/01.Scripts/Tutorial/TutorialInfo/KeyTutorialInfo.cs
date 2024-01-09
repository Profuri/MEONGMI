using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(menuName = "SO/Tutorial/Key")]
public class KeyTutorialInfo : TutorialInfo
{
    [SerializeField] private List<KeyCode> _codeList = new List<KeyCode>();

    private Dictionary<KeyCode, bool> _codeDictionary = new Dictionary<KeyCode,bool>();
    
    private int _codeCnt;
    public void Init()
    {
        _codeDictionary.Clear();
        foreach (KeyCode code in _codeList)
        {
            _codeDictionary.Add(code,false);
        }
    }
    public override void TutorialUpdate()
    {
        if (_isClear) return;
        
        foreach (KeyCode code in _codeDictionary.Keys)
        {
            if (_codeDictionary[code] == false && Input.GetKeyDown(code))
            {
                _codeDictionary[code] = true;
                _codeCnt++;
            }
        }

        if (_codeCnt == _codeDictionary.Count)
        {
            _isClear = true;
            Debug.Log($"IsCleared: {this}");
        }
    }
}
