using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Tutorial/Resource")]
public class ResourceTutorialInfo : TutorialInfo
{
    private ResourceMono _resMono;
    public override void Init()
    {
        
    }

    public override void TutorialUpdate()
    {
        if (_resMono == null)
        {
            _resMono = PoolManager.Instance.Pop("ResourceMono") as ResourceMono;
            _resMono.transform.position = TutorialSpawner.Instance.transform.position;
        }
        else if (_resMono != null && _resMono.gameObject.activeSelf == false)
        {
            _isClear = true;
        }
    }
}
