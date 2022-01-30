using UnityEngine;

public class TriggerFallAction : AbstractTriggerAction
{
    private bool _hasFallen = false;

    public override void CollisionExit(Collision2D col)
    {
        if (_hasFallen) return;

        if (col.gameObject.transform.position.y > gameObject.transform.position.y)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (_hasFallen) return;

        if (!col.collider.CompareTag("Ground")) return;
        
        Debug.Log("TouchDown");
        _hasFallen = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }
}
