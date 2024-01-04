using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cvCam;
    [SerializeField] private Transform _uiParent;
    private StartFadeOutPanel _fadeOutPanel;

    private void Awake()
    {
        _fadeOutPanel = FindObjectOfType<StartFadeOutPanel>();
        
    }

    [ContextMenu("IntoTheCam")]
    public void IntoTheCam()
    {
        _uiParent.gameObject.SetActive(false);
        Sequence seq = DOTween.Sequence();
        seq.Append(_cvCam.transform.DORotate(new Vector3(0, -20f, 0), 2f));
        seq.Join(DOTween.To(() => _cvCam.m_Lens.OrthographicSize, x => _cvCam.m_Lens.OrthographicSize = x, 3f, 2f));
        seq.AppendCallback(() => _fadeOutPanel.LoadAsyncCor(1,null));
        seq.Append(DOTween.To(() => _cvCam.m_Lens.OrthographicSize,x => _cvCam.m_Lens.OrthographicSize = x,1.5f,1.5f));
    }
        
    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
