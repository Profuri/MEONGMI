using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class StartFadeOutPanel : MonoBehaviour
{
    [SerializeField] private Image _fadeUI;
    [SerializeField] private float _timerMinValue;
    [SerializeField] private float _fadeTime = 2f;
    private Vector3 _maxOffset = new Vector3(1.2f, 2.0f, 1f);
    
    public void LoadAsyncCor(int sceneIdx, Action Callback)
    {
        StartCoroutine(AsyncLoadCor(sceneIdx,Callback));
    }
    private IEnumerator AsyncLoadCor(int sceneIdx, Action Callback)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIdx);
        asyncLoad.allowSceneActivation = false;
        
        float timer = 0f;
        _fadeUI.rectTransform.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        seq.Append(_fadeUI.rectTransform.DOScale(_maxOffset, _fadeTime).SetEase(Ease.InOutCubic));
        while (!asyncLoad.isDone || timer < _timerMinValue)
        {
            timer += Time.deltaTime;

            float percent = Mathf.Min(asyncLoad.progress, timer / _timerMinValue);
            
            if (percent >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        seq.Append(_fadeUI.rectTransform.DOScale(Vector3.zero,_fadeTime).SetEase(Ease.InOutCubic));
        

        Callback?.Invoke();
    }
}
