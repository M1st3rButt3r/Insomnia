using UnityEngine;

public abstract class AbstractTriggerAction : MonoBehaviour
{
    public abstract void TriggerAction();

    public virtual void TriggerExit() {}
}
