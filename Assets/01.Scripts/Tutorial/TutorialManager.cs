using System.Collections;
using System.Collections.Generic;
using InputControl;
using UnityEngine;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private List<TutorialInfo> _tutorialList = new List<TutorialInfo>();
    
    
    private int _currentIdx;
    private bool _isPlaying = false;

    private void Awake()
    {
    }
 
    public override void Init()
    {
        for (int i = 0; i < _tutorialList.Count; i++)
        {
            _tutorialList[i] = Instantiate(_tutorialList[i]);
        }

        _isPlaying = true;
        _currentIdx = 0;
    }

    public void Update()
    {
        if (_isPlaying == false) return;

        _tutorialList[_currentIdx].TutorialUpdate();
        if (_tutorialList[_currentIdx].IsClear())
        {
            NextTutorial();
        }
    }

    private void NextTutorial()
    {
        _currentIdx++;
        _currentIdx = Mathf.Clamp(_currentIdx, 0, _tutorialList.Count);

        //CurrentTutorial Index overflow max tutorial count;
        if(_currentIdx == _tutorialList.Count)
        {
            EndTutorial();
        }
        Debug.Log($"CurrentTutorialIdx: {_currentIdx}");
    }

    private void EndTutorial()
    {

    }
}
