using System.Collections;
using System.Collections.Generic;
using InputControl;
using UnityEngine;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private List<TutorialInfo> _tutorialList = new List<TutorialInfo>();
    [SerializeField] private Transform _canvasTrm;
    private TutorialTextPanel _textPanel;

    private TutorialInfo _CurTutorial
    {
        get
        {
            try
            {
                return _tutorialList[_currentIdx];
            }
            catch
            {
                Debug.LogError($"Can't access index: {_currentIdx}");
                return null;
            }
        }
    }
    private int _currentIdx;
    private bool _isPlaying = false;

    private void Awake()
    {
        Init();
    }
 
    public override void Init()
    {
        Transform rootTrm = _canvasTrm.Find("Root");
        Transform textPanelTrm = rootTrm.Find("TextPanel");
        
        _textPanel = textPanelTrm.GetComponent<TutorialTextPanel>();
        _textPanel.Init();
        
        for (int i = 0; i < _tutorialList.Count; i++)
        {
            _tutorialList[i] = Instantiate(_tutorialList[i]);
            _tutorialList[i].Init();
            Debug.Log($"Tutorial Element: {_tutorialList[i]}");
        }

        _isPlaying = true;
        _currentIdx = -1;
        NextTutorial();
    }

    public void Update()
    {
        if (_isPlaying == false || _currentIdx < 0) return;

        _CurTutorial.TutorialUpdate();
        if (_CurTutorial.IsClear())
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
            return;
        }
        
        _textPanel.SetText(_CurTutorial.infoText);
        Debug.Log($"CurrentTutorialIdx: {_currentIdx}");
    }

    private void EndTutorial()
    {
        _isPlaying = false;
        _textPanel.SetText($"수고하셨습니다 튜토리얼을 완료하였습니다 \n 메인화면으로 돌아가 게임을 플레이해보세요!");
    }
}
