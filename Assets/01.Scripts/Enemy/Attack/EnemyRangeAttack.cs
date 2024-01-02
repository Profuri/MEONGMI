using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Bullet _bullet;
    
    public override void AttackHandle()
    {
        base.AttackHandle();
        Vector3 lookPos = _baseEnemy.Target.position - _baseEnemy.transform.position;
        lookPos.y = 0f;
        _baseEnemy.transform.rotation = Quaternion.LookRotation(lookPos);
    }
    protected override void Attack()
    {
        base.Attack();
        
        Vector3 startPos = _muzzle.position;
        
        Debug.Log($"StartPos: {startPos}");
        Bullet bullet = PoolManager.Instance.Pop(_bullet.gameObject.name) as Bullet;
        bullet.Init();
        bullet.Setting(BulletType.Enemy, _baseEnemy.EnemyAttackSO.damage, startPos, _baseEnemy.transform.forward);
        Debug.Log($"BulletPos: {bullet.transform.position}");

    }
}
