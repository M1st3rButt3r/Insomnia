using UnityEngine;

public class TriggerFallAction : AbstractTriggerAction
{
    public override void CollisionAction()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
