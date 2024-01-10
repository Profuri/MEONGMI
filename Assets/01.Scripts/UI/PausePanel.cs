using System;
using DG.Tweening;
using InputControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private SettingPanel _setting;
    
    public void Generate()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        DOTween.To(() => _layoutGroup.spacing, x => _layoutGroup.spacing = x, 120, 0.25f).SetUpdate(true);
    }

    public void Remove(Action callBack = null)
    {
        DOTween.To(() => _layoutGroup.spacing, x => _layoutGroup.spacing = x, 1000, 0.25f).SetUpdate(true).OnComplete(
            () =>
            {
                Time.timeScale = 1;
                gameObject.SetActive(false);
                callBack?.Invoke();
            }
        );
    }
    
    public void Setting()
    {
        _setting.Generate();
    }

    public void Restart()
    {
        Remove();
    }

    public void Quit()
    {
        Remove(() =>
        {
            _inputReader.ClearInputEvent();
            SceneManagement.Instance.LoadScene("Start");
            SceneManagement.Instance.OnRestartGameEvent?.Invoke();
        });
    }
}