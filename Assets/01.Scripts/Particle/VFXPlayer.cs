using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXPlayer : PoolableMono
{
    private VisualEffect _visualEffect;
    
    public override void Init()
    {
        _visualEffect = GetComponentInChildren<VisualEffect>();
    }
    
    public void PlayEffect()
    {
        StopEffect();
        _visualEffect.Play();
    }
    
    public void StopEffect()
    {
        _visualEffect.Stop();
    }


}
