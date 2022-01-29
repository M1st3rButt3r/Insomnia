using System;
using UnityEngine;

class BedInteraction : AbstractTriggerAction
{
    private bool _canInteract = false;

    private void Start()
    {
        PlayerInput.Instance.Interact += Interact;
    }
    
    public override void TriggerAction()
    {
        _canInteract = true;
    }

    public override void TriggerExit()
    {
        _canInteract = false;
    }

    private void Interact()
    {
        if (! _canInteract)
        {
            return;
        }
        
        Debug.Log("E pressed on NON bed interact");
    }
}
