using System;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] private LayerMask _damagableMask;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _bulletSpeed;
    
    private Rigidbody _rigidbody;
    private BulletType _bulletType;

    private Vector3 _dir;

    public void Setting(BulletType type, Vector3 pos, Vector3 dir)
    {
        _bulletType = type;
        _dir = dir;
        transform.position = pos;
        _rigidbody.velocity = dir * _bulletSpeed;
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
        
            var particle = PoolManager.Instance.Pop($"{_bulletType.ToString()}Hit") as PoolableParticle;
            particle.SetPositionAndRotation(transform.position);
            particle.Play();
            
            PoolManager.Instance.Push(this);
        }
    }

    private bool DamageCheck(out Collider[] cols, out int cnt)
    {
        cols = new Collider[10];
        cnt = Physics.OverlapSphereNonAlloc(transform.position, _checkRadius, cols, _damagableMask);
        return cnt > 0;
    }
}