using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Text;
using TMPro;
using UnityEngine.InputSystem;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private float _timerMinValue = 5f;
    [SerializeField] private Slider _slider;
    [SerializeField] private int _gameSceneIdx;

    private TextMeshProUGUI _sliderText;
    
    private void Start()
    {
        _sliderText = _slider.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        int sceneIdx = _gameSceneIdx;
        StartCoroutine(AsyncLoadCor(_gameSceneIdx,null));

    }
    private IEnumerator AsyncLoadCor(int sceneIdx, Action Callback)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIdx);
        asyncLoad.allowSceneActivation = false;
        
        float timer = 0f;

        StringBuilder sb = new StringBuilder();
        while (!asyncLoad.isDone || timer < _timerMinValue)
        {
            Debug.Log($"IsDone: {asyncLoad.isDone}");
            Debug.Log($"Progress {asyncLoad.progress}");
            timer += Time.deltaTime;
            sb.Remove(0, sb.Length);
            
            float percent = asyncLoad.progress < timer / _timerMinValue ? asyncLoad.progress : timer / _timerMinValue;
            
            _slider.value = percent;
            sb.Append($"Loading...{percent * 100f}%");
            
            _sliderText.SetText(sb.ToString());
            if (percent >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        Callback?.Invoke();
    }
}
