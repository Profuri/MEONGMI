using System.Collections.Generic;
using UnityEngine;

public class PierceBullet : Bullet
{
    private List<Entity> _damagedEntity;

    public override void Init()
    {
        base.Init();
        _damagedEntity = new List<Entity>();
    }

    public override void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_dir), 0.1f);
        _rigidbody.velocity = transform.forward * _bulletSpeed;

        if (DamageCheck(out var cols, out var cnt))
        {
            for (var i = 0; i < cnt; i++)
            {
                if (cols[i].TryGetComponent<Entity>(out var entity))
                {
                    if (!entity.Dead && !_damagedEntity.Find(x => x.GetHashCode() == entity.GetHashCode()))
                    {
                        entity.Damaged(_damage);
                        var particle = PoolManager.Instance.Pop($"{_bulletType.ToString()}Hit") as PoolableParticle;
                        particle.SetPositionAndRotation(entity.transform.position);
                        particle.Play();
                        _damagedEntity.Add(entity);
                    }
                }    
            }
        }
        
        _currentTime += Time.deltaTime;
        if (_currentTime >= _bulletDestroyDelay)
        {
            ExplosionBullet();
        }
    }
}