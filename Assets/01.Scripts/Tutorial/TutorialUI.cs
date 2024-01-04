using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Transform _uiParent;
    [SerializeField] private Button _uiBtn;

    [SerializeField] private List<Sprite> _imageList = new List<Sprite>();
    private Image _showImage;
    private Button _leftArrowBtn;
    private Button _rightArrowBtn;

    private int _currentIdx;


    private void Awake()
    {
        _currentIdx = 0;
        
        _leftArrowBtn = _uiParent.Find("Buttons/LeftArrowBtn").GetComponent<Button>();
        _rightArrowBtn = _uiParent.Find("Buttons/RightArrowBtn").GetComponent<Button>();
        _showImage = _uiParent.Find("ShowImage").GetComponent<Image>();
        
        _leftArrowBtn.onClick.AddListener(() => LoadPrevImage());
        _rightArrowBtn.onClick.AddListener(() => LoadNextImage());

        UpdateUI();
        CloseUI();
    }


    public void LoadNextImage()
    {
        if (_currentIdx >= _imageList.Count - 1) return;
        _currentIdx++;
        _currentIdx = Mathf.Clamp(_currentIdx,0,_imageList.Count);
        UpdateUI();
    }

    public void LoadPrevImage()
    {
        if (_currentIdx == 0) return;
        _currentIdx++;
        _currentIdx = Mathf.Clamp(_currentIdx,0,_imageList.Count);
        UpdateUI();
    }

    public void OpenUI()
    {
        _uiBtn.gameObject.SetActive(false);
        _uiParent.gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        _uiBtn.gameObject.SetActive(true);
        _uiParent.gameObject.SetActive(false);
    }
    private void UpdateUI()
    {
        if (_imageList.Count == 0)
        {
            _leftArrowBtn.enabled = false;
            _rightArrowBtn.enabled = false;
            return;
        }
        
        _showImage.sprite = _imageList[_currentIdx];

        if (_currentIdx == 0)
        {
            _leftArrowBtn.enabled = false;
        }
        else if (_currentIdx == _imageList.Count - 1)
        {
            _rightArrowBtn.enabled = false;
        }
        else
        {
            _leftArrowBtn.enabled = true;
            _rightArrowBtn.enabled = true;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
