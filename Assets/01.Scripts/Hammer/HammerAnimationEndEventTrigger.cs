using System;
using UnityEngine;

public class HammerAnimationEndEventTrigger : MonoBehaviour
{
    public event Action AnimationEndEvent = null;
    
    public void AnimationEndTrigger()
    {
        AnimationEndEvent?.Invoke();
    }
}