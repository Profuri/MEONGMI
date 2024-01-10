using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tutorial/Enemy")]
public class EnemyTutorialInfo : TutorialInfo
{
    private List<BaseEnemy> _enemyList;
    private bool _isStart = false;
    public override void Init()
    {
        _enemyList = new List<BaseEnemy>();
    }

    public override void TutorialUpdate()
    {
        if (_isStart == false)
        {
            _enemyList = TutorialSpawner.Instance.SpawnEnemies();
            _isStart = true;
        }

        int aliveCnt = _enemyList.Count;

        foreach (BaseEnemy enemy in _enemyList)
        {
            if (enemy.gameObject.activeSelf == false)
            {
                aliveCnt--;
            }
        }
        if (aliveCnt <= 0)
        {
            _isClear = true;
        }
    }
}
