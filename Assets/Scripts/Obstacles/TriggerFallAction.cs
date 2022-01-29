using UnityEngine;

public class TriggerFallAction : AbstractTriggerAction
{
    public override void TriggerAction()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
