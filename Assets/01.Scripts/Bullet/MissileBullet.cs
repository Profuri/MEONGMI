using UnityEngine;

public class MissileBullet : Bullet
{
    [SerializeField] private float _missileDetectRadius;

    private Transform _target;
    
    public override void Update()
    {
        _target = TargetCheck();
        if (_target is not null)
        {
            _dir = (_target.position - transform.position).normalized;
        }
        base.Update();
    }
    
    private Transform TargetCheck()
    {
        var cols = new Collider[10];
        var cnt = Physics.OverlapSphereNonAlloc(transform.position, _checkRadius, cols, _damagableMask);
        return cnt > 0 ? cols[0].transform : null;
    }
}