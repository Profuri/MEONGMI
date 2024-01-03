using System;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private float _distanceInterval;
    [SerializeField] private float _angleInterval;
    
    private Animator _animator;
    private HammerAnimationEndEventTrigger _eventTrigger;

    private Transform _shotPoint;
    private PlayerController _playerController;

    private readonly int _shotTriggerHash = Animator.StringToHash("Shot");
    private readonly int _gatheringTriggerHash = Animator.StringToHash("Gathering");
    private readonly int _chargingToggleHash = Animator.StringToHash("Charging");

    private void Awake()
    {
        var visualTrm = transform.Find("Visual");
        _shotPoint = visualTrm.Find("ShotPoint");
        _animator = visualTrm.GetComponent<Animator>();
        _eventTrigger = visualTrm.GetComponent<HammerAnimationEndEventTrigger>();
    }

    public void Shot(BulletType type, Vector3 dir)
    {
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
                var attackDir = _shotPoint.up;
                attackDir.y = 0;
                bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + attackDir * 0.5f, attackDir);
                
                _shotPoint.localPosition = new Vector3(0, 3, i * _distanceInterval);
                bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
                attackDir = _shotPoint.up;
                attackDir.y = 0;
                bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + attackDir * 0.5f, attackDir);
            }
        }
        else
        {
            var bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
            var attackDir = _shotPoint.up;
            attackDir.y = 0;
            bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + attackDir * 0.5f, attackDir);

            for (var i = 1; i <= cnt / 2; i++)
            {
                _shotPoint.localEulerAngles = new Vector3(-i * _angleInterval, 0, 0);
                bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
                attackDir = _shotPoint.up;
                attackDir.y = 0;
                bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + attackDir * 0.5f, attackDir);
                
                _shotPoint.localEulerAngles = new Vector3(i * _angleInterval, 0, 0);
                bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
                attackDir = _shotPoint.up;
                attackDir.y = 0;
                bullet.Setting(type, _playerController.PlayerStat.damage.GetValue(), _shotPoint.position + attackDir * 0.5f, attackDir);
            }
        }
        
        CameraManager.Instance.ImpulseCam(0.25f, 0.15f, new Vector3(1, 1, 0));
    }

    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
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