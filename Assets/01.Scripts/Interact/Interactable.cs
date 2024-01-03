using UnityEngine;

public abstract class Interactable : PoolableMono
{
    [SerializeField] protected float _interactRadius;
    public float InteractRadius => _interactRadius;
    
    public abstract void OnInteract(Entity entity);
    
    public override void Init()
    {
        
    }
}