using UnityEngine;

public class BedInteraction : AbstractTriggerAction
{
    protected bool CanInteract;

    private void Start()
    {
        PlayerInput.Instance.Interact += Interact;
    }
    
    public override void CollisionAction()
    {
        CanInteract = true;
    }

    public override void TriggerExit()
    {
        CanInteract = false;
    }

    protected virtual void Interact()
    {
        if (! CanInteract)
        {
            return;
        }
        
        Debug.Log($"E pressed on bed interact (Name: {gameObject.name})");
    }
}
