using UnityEngine;

public class TriggerFallToSideAction : AbstractTriggerAction, IResettable
{
    [SerializeField] private float fallingAcceleration;

    private bool _isFalling = false;
    private float _fallingSpeed = 0.0f;

    public float targetAngle;

    private float _offsetToRotation;

    private Quaternion initialRot;

    private void Start()
    {   
        _offsetToRotation = this.transform.rotation.eulerAngles.z;
        initialRot = this.transform.rotation;
        GameManager.Instance.AddToResettables(this);
    }

    private void Update()
    {
        if (!_isFalling)
        {
            return;
        }

        if (transform.rotation.eulerAngles.z <= targetAngle + _offsetToRotation && transform.rotation.eulerAngles.z > 0 + _offsetToRotation)
        {
            _isFalling = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle + _offsetToRotation));
            return;
        }

        _fallingSpeed += fallingAcceleration * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, -_fallingSpeed * Time.deltaTime));
    }

    public override void CollisionAction()
    {
        _isFalling = true;
    }

    public void ResetAsset()
    {
        _isFalling = false;
        _fallingSpeed = 0;
        transform.rotation = initialRot;
    }
}