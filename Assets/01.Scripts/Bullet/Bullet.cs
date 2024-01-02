using System;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] protected LayerMask _damagableMask;
    [SerializeField] protected float _checkRadius;
    
    protected Rigidbody _rigidbody;
    protected BulletType _bulletType;
    
    public void Setting(BulletType type, Vector3 pos, Vector3 dir, float speed)
    {
        _bulletType = type;
        transform.position = pos;
        _rigidbody.velocity = dir * speed;
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
        if (DamageCheck(out var cols, out var cnt))
        {
            for (var i = 0; i < cnt; i++)
            {
                // damage logic    
            }
                    
            //var particle = PoolManager.Instance.Pop($"{_bulletType.ToString()}BulletHit") as PoolableParticle;
            //particle.SetPositionAndRotation(transform.position);
            //particle.Play();
            
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