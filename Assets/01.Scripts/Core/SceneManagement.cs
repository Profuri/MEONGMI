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
    public event Action OnTutorialStartEvent;
    public Action OnRestartGameEvent;
    
    [SerializeField] private int _gameSceneIdx;
    
    public override void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if (scene.buildIndex == _gameSceneIdx)
        {
            Debug.Log("OnGameStartEvent");
            OnGameStartEvent?.Invoke();
        }

        if (scene.name == "Tutorial")
        {
            Debug.Log("OnTutorialStartEvent");
            OnTutorialStartEvent?.Invoke();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
