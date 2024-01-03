using UnityEngine;

public class PierceBullet : Bullet
{
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
                    entity.Damaged(_damage);
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