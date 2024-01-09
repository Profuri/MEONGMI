using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBullet : Bullet
{
    [SerializeField] float damageUpPercent = 2f;
    [SerializeField] float bombRadius = 4f;

    public override void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_dir), 0.1f);
        _rigidbody.velocity = transform.forward * _bulletSpeed;

        if (DamageCheck(out var cols, out var cnt))
        {
            for (var i = 0; i < cnt; i++)
            {
                if (cols[i].TryGetComponent<IDamageable>(out var entity))
                {
                    entity.Damaged(_damageType, _damage * damageUpPercent);
                }
            }

            ExplosionBullet();
        }

        _currentTime += Time.deltaTime;
        if (_currentTime >= _bulletDestroyDelay)
        {
            ExplosionBullet();
        }
    }

    protected override void ExplosionBullet()
    {
        base.ExplosionBullet();

        Collider[] cols = Physics.OverlapSphere(transform.position, bombRadius, _damagableMask);

        for (var i = 0; i < cols.Length; i++)
        {
            if (cols[i].TryGetComponent<Entity>(out var entity))
            {
                entity.Damaged(_damageType, _damage);

                var particle = PoolManager.Instance.Pop($"{_bulletType.ToString()}Hit") as PoolableParticle;
                particle.SetPositionAndRotation(cols[i].transform.position);
                particle.Play();
            }
        }
    }
}
