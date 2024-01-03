using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManagement.Instance.LoadScene(sceneName);
    }
}
