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

    public void Setting(BulletType type, float damage, Vector3 pos, Vector3 dir)
    {
        _bulletType = type;
        _damage = damage;
        _dir = dir;
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(dir);
        _currentTime = 0f;
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
                    entity.Damaged(_damage);
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
        
        PoolManager.Instance.Push(this);
    }

    protected bool DamageCheck(out Collider[] cols, out int cnt)
    {
        cols = new Collider[10];
        cnt = Physics.OverlapSphereNonAlloc(transform.position, _checkRadius, cols, _damagableMask);
        return cnt > 0;
    }
}