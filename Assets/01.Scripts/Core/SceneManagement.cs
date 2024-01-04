using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum ESceneName
{
    Start,
    Game,
    End
}

public class SceneManagement : MonoSingleton<SceneManagement>
{
    public event Action OnGameStartEvent;
    [SerializeField] private int _gameSceneIdx;
    
    public override void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if (scene.buildIndex ==_gameSceneIdx)
        {
            Debug.Log("OnGameStartEvent");
            OnGameStartEvent?.Invoke();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
