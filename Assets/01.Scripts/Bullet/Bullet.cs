using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] protected LayerMask _damagableMask;
    [SerializeField] protected float _checkRadius;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _bulletDestroyDelay = 3f;
    
    protected Rigidbody _rigidbody;
    protected BulletType _bulletType;

    protected Vector3 _dir;
    protected float _currentTime;

    protected float _damage;

    protected DamageType _damageType;

    public void Setting(BulletType type, float damage, Vector3 pos, Vector3 dir)
    {
        _bulletType = type;
        _damageType = SetDamageType(_bulletType);
        _damage = damage;
        _dir = dir;
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(dir);
        _currentTime = 0f;
    }

    private DamageType SetDamageType(BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.Normal:
            case BulletType.Missile:
            case BulletType.Transition:
            case BulletType.Material:
            case BulletType.Pierce:
            case BulletType.Enemy:
                return DamageType.HandleByAttacker;
            case BulletType.Slow:
            case BulletType.Poison:
                return DamageType.HandleByReciver;
        }
        return DamageType.None;
    }

    public override void Init()
    {
        if (_rigidbody is null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    public virtual void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_dir), 0.1f);
        _rigidbody.velocity = transform.forward * _bulletSpeed;

        if (DamageCheck(out var cols, out var cnt))
        {
            for (var i = 0; i < cnt; i++)
            {
                if (cols[i].TryGetComponent<IDamageable>(out var entity))
                {
                    entity.Damaged(_damageType, _damage);
                    if(_damageType == DamageType.HandleByReciver)
                    {
                        BaseEnemy enemy = entity as BaseEnemy;
                        if (enemy != null)
                            enemy.SetDebuff(_bulletType, _damage);
                    }
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

    protected virtual void ExplosionBullet()
    {
        var particle = PoolManager.Instance.Pop($"{_bulletType.ToString()}Hit") as PoolableParticle;
        particle.SetPositionAndRotation(transform.position);
        particle.Play();
        SoundManager.Instance.PlaySFX("EnemyHitEffect");
        PoolManager.Instance.Push(this);
    }

    protected bool DamageCheck(out Collider[] cols, out int cnt)
    {
        cols = new Collider[10];
        cnt = Physics.OverlapSphereNonAlloc(transform.position, _checkRadius, cols, _damagableMask);
        return cnt > 0;
    }
}