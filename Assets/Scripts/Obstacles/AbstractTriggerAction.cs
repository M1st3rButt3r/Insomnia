using UnityEngine;

public abstract class AbstractTriggerAction : MonoBehaviour
{
    public virtual void CollisionAction() {}

    public virtual void CollisionExit() {}
    
    public virtual void TriggerAction() {}
    
    public virtual void TriggerExit() {}
}
