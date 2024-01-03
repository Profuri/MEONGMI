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
    public override void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if (scene.name == ESceneName.Game.ToString())
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
