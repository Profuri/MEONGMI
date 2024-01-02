using UnityEngine;

public class TransitionBullet : Bullet
{
    [SerializeField] private float _transitionRadius;
    [SerializeField] private int _transitionCnt;

    protected override void ExplosionBullet()
    {
        Transition();
        base.ExplosionBullet();
    }

    private void Transition()
    {
        var cols = new Collider[_transitionCnt];
        var cnt = Physics.OverlapSphereNonAlloc(transform.position, _transitionRadius, cols, _damagableMask);

        for (var i = 0; i < cnt; i++)
        {
            if (cols[i].TryGetComponent<Entity>(out var entity))
            {
                entity.Damaged(_damage);

                var particle = PoolManager.Instance.Pop($"{_bulletType.ToString()}Hit") as PoolableParticle;
                particle.SetPositionAndRotation(cols[i].transform.position);
                particle.Play();
            }
        }
    }
}