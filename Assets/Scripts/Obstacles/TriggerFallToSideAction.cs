using System;
using UnityEngine;

public class TriggerFallToSideAction : AbstractTriggerAction
{
    [SerializeField] private float fallingAcceleration;

    private bool _isFalling = false;
    private float _fallingSpeed = 0.0f;

    private float _offsetToRotation;

    private void Awake()
    {
        _offsetToRotation = this.transform.rotation.eulerAngles.z;
    }

    private void Update()
    {
        if (!_isFalling)
        {
            return;
        }

        if (transform.rotation.eulerAngles.z <= 270 + _offsetToRotation && transform.rotation.eulerAngles.z > 0 + _offsetToRotation)
        {
            _isFalling = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270 + _offsetToRotation));
            return;
        }

        _fallingSpeed += fallingAcceleration * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, -_fallingSpeed * Time.deltaTime));
    }

    public override void TriggerAction()
    {
        _isFalling = true;
    }
}