using System;
using UnityEngine;

public class TriggerMoveAction : AbstractTriggerAction, IResettable
{
    [SerializeField] private Vector3 moveToPosition;
    [SerializeField] private float speed;
    [SerializeField] private float spinRot;
    [SerializeField] private bool spins;

    private bool _theySeeMeRolling;

    private Transform initialTransform;

    private void Start()
    {
        initialTransform = this.transform;
        GameManager.Instance.AddToResettables(this);
    }

    private void Update()
    {
        if (!_theySeeMeRolling || Vector3.Distance(moveToPosition, transform.position) < 0.1)
        {
            return;
        }

        Vector3 difference = moveToPosition - transform.position;

        this.transform.position += difference.normalized * speed * Time.deltaTime;

        if (spins)
        {
            transform.Rotate(new Vector3(0, 0,spinRot * Time.deltaTime * 180 / Mathf.PI * GetComponent<CircleCollider2D>().radius));
        }
    }

    public override void CollisionAction()
    {
        _theySeeMeRolling = true;
    }

    public void ResetAsset()
    {
        _theySeeMeRolling = false;
        this.transform.position = initialTransform.position;
        this.transform.rotation = initialTransform.rotation;
    }
}