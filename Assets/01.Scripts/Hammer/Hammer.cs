using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class ColorType
{
    [ColorUsage(true,true)] public Color color;
    public BulletType type;
}
public class Hammer : MonoBehaviour
{
    [SerializeField] private float _distanceInterval;
    [SerializeField] private float _angleInterval;
    
    private Animator _animator;
    private HammerAnimationEndEventTrigger _eventTrigger;

    private Transform _shotPoint;
    private PlayerController _playerController;

    private MeshRenderer _meshRenderer;

    private readonly int _shotTriggerHash = Animator.StringToHash("Shot");
    private readonly int _gatheringTriggerHash = Animator.StringToHash("Gathering");
    private readonly int _chargingToggleHash = Animator.StringToHash("Charging");

    [SerializeField] private List<ColorType> _colorTypeList = new List<ColorType>();
    //private readonly int _emission = Shader.PropertyToID("_EmissionColor");

    public Color GetColorByBulletType(BulletType type)
    {
        foreach (ColorType ct in _colorTypeList)
        {
            if (ct.type == type)
            {
                return ct.color;
            }
        }
        Debug.LogError("Can't Find Color");
        return Color.blue;
    }

    private void SetEmissionColor(BulletType type)
    {
        MaterialPropertyBlock matblock = new MaterialPropertyBlock();
        _meshRenderer.GetPropertyBlock(matblock);
        //matblock.SetColor();
        matblock.SetColor("_EmissionColor",GetColorByBulletType(type));
        _meshRenderer.SetPropertyBlock(matblock);
    }
    private void Awake()
    {
        var visualTrm = transform.Find("Visual");
        _shotPoint = visualTrm.Find("ShotPoint");
        _animator = visualTrm.GetComponent<Animator>();
        _eventTrigger = visualTrm.GetComponent<HammerAnimationEndEventTrigger>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    
    
    public void Shot(BulletType type, Vector3 dir)
    {
        SetEmissionColor(type);
        var particle = PoolManager.Instance.Pop($"{type.ToString()}Flash") as PoolableParticle;
        particle.SetPositionAndRotation(_shotPoint.position, Quaternion.LookRotation(dir));
        particle.Play();

        var cnt = _playerController.PlayerStat.shotCnt.GetValue();
        _shotPoint.localEulerAngles = Vector3.zero;
        _shotPoint.localPosition = new Vector3(0, 3, 0);

        if (cnt % 2 == 0)
        {
            for (var i = 1; i <= cnt / 2; i++)
            {
                _shotPoint.localPosition = new Vector3(0, 3, -i * _distanceInterval);
                var bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
                bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + _shotPoint.up * 0.5f, _shotPoint.up);
                
                _shotPoint.localPosition = new Vector3(0, 3, i * _distanceInterval);
                bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
                bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + _shotPoint.up * 0.5f, _shotPoint.up);
            }
        }
        else
        {
            var bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
            bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + _shotPoint.up * 0.5f, _shotPoint.up);

            for (var i = 1; i <= cnt / 2; i++)
            {
                _shotPoint.localEulerAngles = new Vector3(-i * _angleInterval, 0, 0);
                bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
                bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + _shotPoint.up * 0.5f, _shotPoint.up);
                
                _shotPoint.localEulerAngles = new Vector3(i * _angleInterval, 0, 0);
                bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
                bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + _shotPoint.up * 0.5f, _shotPoint.up);
            }
        }
        
        CameraManager.Instance.ImpulseCam(0.25f, 0.15f, new Vector3(1, 1, 0));
    }

    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.OnBulletTypeChanged += SetEmissionColor;
    }

    public void ChargingToggle(bool value)
    {
        _animator.SetBool(_chargingToggleHash, value);
    }

    public void GatheringTrigger()
    {
        _animator.SetTrigger(_gatheringTriggerHash);
    }

    public void ShotTrigger()
    {
        _animator.SetTrigger(_shotTriggerHash);
    }

    public void SetAnimationSpeed(float speed)
    {
        _animator.speed = speed;
    }

    public void ResetAnimationSpeed()
    {
        _animator.speed = 1f;
    }

    public void AddAnimationEndEvent(Action endCallBack)
    {
        _eventTrigger.AnimationEndEvent += endCallBack;
    }
    
    public void RemoveAnimationEndEvent(Action endCallBack)
    {
        _eventTrigger.AnimationEndEvent -= endCallBack;
    }
    
}