using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class TutorialEnemy : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    private Transform _target;
    private Transform _cylinder;
    private float _currentHP;

    [SerializeField] private EntityStatSO _statSO;
    

    private TutorialEnemyAnimator _enemyAnimator;
    
    public void Init()
    {
        gameObject.SetActive(true);
        _target = TutorialManager.Instance.PlayerTrm;
        _cylinder = transform.Find("Cylinder");
        _enemyAnimator = GetComponentInChildren<TutorialEnemyAnimator>();
        _navAgent = GetComponent<NavMeshAgent>();
        
        
        _currentHP = _statSO.maxHp;
        _navAgent.speed = _statSO.moveSpeed;

    }

    private void DeadProcess()
    {
        _cylinder.gameObject.SetActive(false);
        _navAgent.speed = 0f;
        _navAgent.enabled = false;
        _enemyAnimator.OnDeadEvent += TempDissolve;
    }

    private void TempDissolve()
    {
        _enemyAnimator.StartDissolveCor(0f,1f,0.8f,() => Destroy(this.gameObject));
    }

    public void Damaged(DamageType type, float damage)
    {
        _currentHP -= damage;
        _currentHP = Mathf.Clamp(_currentHP, 0, _statSO.maxHp);
        _enemyAnimator.StartBlinkCoroutine(0f,1f,0.1f,null);
        
        if (_currentHP == 0)
        {

        }
    }
}
