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
    public override void Init()
    {
        _codeDictionary.Clear();
        foreach (KeyCode code in _codeList)
        {
            _codeDictionary.Add(code,false);
        }

        _codeCnt = 0;
        _isClear = false;
    }
    public override void TutorialUpdate()
    {
        if (_isClear) return;

        List<KeyCode> curCodeList = _codeDictionary.Keys.ToList();
        foreach (KeyCode code in curCodeList)
        {
            if (_codeDictionary[code] == false && Input.GetKeyDown(code))
            {
                _codeDictionary[code] = true;
                _codeCnt++;
            }
        }

        if (_codeCnt >= _codeDictionary.Count)
        {
            Debug.Log($"CodeCnt: {_codeCnt} DictionaryCnt: {_codeDictionary.Count}");
            _isClear = true;
            Debug.Log($"IsCleared: {this}");
        }
    }
}
