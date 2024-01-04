using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameHUD : UIComponent
{
    [SerializeField] private RectTransform _upperUI;
    [SerializeField] private RectTransform _downUI;
    
    [SerializeField] private TextMeshProUGUI baseResourceText;

    [SerializeField] private TextMeshProUGUI UnitText;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI enemyText;

    [SerializeField] private GameObject timePanel;
    [SerializeField] private GameObject enemyPanel;

    [SerializeField] private Slider playerResSlider;
    [SerializeField] private Slider baseResSlider;
    [SerializeField] private Slider unitSlider;
    [SerializeField] private Slider playerHpSlider;

    [SerializeField] private Ease sliderEase;

    public override void GenerateUI(Transform parent)
    {
        base.GenerateUI(parent);

        PhaseManager.Instance.OnPhaseChange += ChangeTextPanel;
        
        ResManager.Instance.OnChangeBaseRes += UpdateBaseResource;
        ResManager.Instance.OnChangePlayerRes += UpdatePlayerResource;

        GameManager.Instance.PlayerController.OnDamaged += UpdatePlayerHp;
        
        ReSet();
    }

    public override void RemoveUI(Action callback)
    {
        base.RemoveUI(callback);
        
        PhaseManager.Instance.OnPhaseChange -= ChangeTextPanel;
        
        PhaseManager.Instance.OnPhaseTimer -= UpdatePhaseTime;
        EnemySpawner.Instance.OnEnemyDead -= UpdateEnemyCnt;
        
        ResManager.Instance.OnChangeBaseRes -= UpdateBaseResource;
        ResManager.Instance.OnChangePlayerRes -= UpdatePlayerResource;
            
        GameManager.Instance.PlayerController.OnDamaged -= UpdatePlayerHp;
    }

    protected override void GenerateTransition()
    {
        _upperUI.DOAnchorPos(new Vector2(0, 0), 0.5f);
        _downUI.DOAnchorPos(new Vector2(0, 0), 0.5f);
    }

    protected override void RemoveTransition(Action callback)
    {
        Debug.Log(1);
        _upperUI.DOAnchorPos(new Vector2(0, 200), 0.5f);
        _downUI.DOAnchorPos(new Vector2(0, -400), 0.5f).OnComplete(() =>
        {
            Debug.Log(2);
            callback?.Invoke();
        });
    }

    private void ChangeTextPanel(PhaseType type)
    {
        if (type == PhaseType.Rest)
        {
            SetTimeText();
            PhaseManager.Instance.OnPhaseTimer += UpdatePhaseTime;
            EnemySpawner.Instance.OnEnemyDead -= UpdateEnemyCnt;
        }
        else
        {
            SetEnemyText();
            PhaseManager.Instance.OnPhaseTimer -= UpdatePhaseTime;
            EnemySpawner.Instance.OnEnemyDead += UpdateEnemyCnt;
        }
    }

    private void SetTimeText()
    {
        enemyPanel.SetActive(false);
        timePanel.SetActive(true);
    }

    private void SetEnemyText()
    {
        enemyPanel.SetActive(true);
        timePanel.SetActive(false);
    }

    private void ReSet()
    {
        ChangeTextPanel(PhaseManager.Instance.PhaseType);
        UpdatePlayerHp();
        UpdateUnitText();
    }

    private void UpdateBaseResource(int cnt)
    {
        int maxRes = StatManager.Instance.MaxBaseResValue;
        UpdateSlider(baseResSlider, cnt, maxRes);
        baseResourceText.text = $"{cnt} / {maxRes}";
    }

    private void UpdatePlayerResource(int cnt)
    {
        int maxRes = StatManager.Instance.MaxBaseResValue;
        UpdateSlider(playerResSlider, cnt, maxRes);
        //playerResourceText.text = curRes.ToString();
    }

    private void UpdatePlayerHp()
    {
        int curHP = (int)GameManager.Instance.PlayerController.CurrentHP;
        int maxHP = (int)GameManager.Instance.PlayerController.GetMaxHP();
        UpdateSlider(playerHpSlider, curHP, maxHP);
        //playerResourceText.text = curRes.ToString();
    }

    private void UpdateUnitText()
    {
        int curRes = GameManager.Instance.Base.CurUnitCount;
        int maxRes = StatManager.Instance.UnitMaxValue;
        UpdateSlider(unitSlider, curRes, maxRes);
        //UnitText.text = $"{curRes} / {maxRes}";
        UnitText.text = curRes.ToString();
    }

    private void UpdatePhaseTime(int remainTime)
    {
        timeText.text = $"{remainTime:0}s";
    }

    private void UpdateEnemyCnt(int remainEnemyCnt)
    {
        enemyText.text = $"{remainEnemyCnt:0}";
    }

    private void UpdateSlider(Slider slider, float minValue, float maxValue, float time = 1f)
    {
        float start = slider.value;
        DOTween.To(() => start, value => slider.value = value, minValue / maxValue, time).SetEase(sliderEase);
    }
}
