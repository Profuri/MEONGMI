using System;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Animator _animator;
    private HammerAnimationEndEventTrigger _eventTrigger;

    private Transform _shotPoint;

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

    public void Shot(BulletType type, Vector3 dir, float speed)
    {
        var particle = PoolManager.Instance.Pop($"{type.ToString()}BulletFlash") as PoolableParticle;
        particle.SetPositionAndRotation(_shotPoint.position, Quaternion.LookRotation(dir));
        particle.Play();

        var bullet = PoolManager.Instance.Pop($"{type.ToString()}Bullet") as Bullet;
        bullet.Setting(type, _shotPoint.position, dir, speed);
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