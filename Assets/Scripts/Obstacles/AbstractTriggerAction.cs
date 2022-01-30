using UnityEngine;

public abstract class AbstractTriggerAction : MonoBehaviour
{
    public virtual void CollisionAction() {}

    public virtual void CollisionExit(Collision2D col) {}
    
    public virtual void TriggerAction() {}
    
    public virtual void TriggerExit() {}
}
