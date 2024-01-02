using System;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] private LayerMask _damagableMask;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDestroyDelay = 3f;
    
    private Rigidbody _rigidbody;
    private BulletType _bulletType;

    private Vector3 _dir;
    private float _currentTime;

    public void Setting(BulletType type, Vector3 pos, Vector3 dir)
    {
        _bulletType = type;
        _dir = dir;
        transform.position = pos;
        _rigidbody.velocity = dir * _bulletSpeed;
        _currentTime = 0f;
    }
    
    public override void Init()
    {
        if (_rigidbody is null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(_dir);

        if (DamageCheck(out var cols, out var cnt))
        {
            for (var i = 0; i < cnt; i++)
            {
                // damage logic    
            }

            if (_bulletType != BulletType.Pierce)
            {
                ExplosionBullet();
            }
        }
        
        _currentTime += Time.deltaTime;
        if (_currentTime >= _bulletDestroyDelay)
        {
            ExplosionBullet();
        }
    }

    private void ExplosionBullet()
    {
        var particle = PoolManager.Instance.Pop($"{_bulletType.ToString()}Hit") as PoolableParticle;
        particle.SetPositionAndRotation(transform.position);
        particle.Play();
        
        PoolManager.Instance.Push(this);
    }

    private bool DamageCheck(out Collider[] cols, out int cnt)
    {
        cols = new Collider[10];
        cnt = Physics.OverlapSphereNonAlloc(transform.position, _checkRadius, cols, _damagableMask);
        return cnt > 0;
    }
}