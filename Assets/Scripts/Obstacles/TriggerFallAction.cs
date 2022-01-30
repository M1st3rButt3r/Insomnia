using System;
using UnityEngine;

public class TriggerFallAction : AbstractTriggerAction, IResettable
{
    private bool _hasFallen = false;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;
        GameManager.Instance.AddToResettables(this);
    }

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
        
        _hasFallen = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void ResetAsset()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.transform.rotation = initialRotation;
        this.transform.position = initialPosition;
        _hasFallen = true;
    }
}
