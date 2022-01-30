using UnityEngine;

public class TriggerFallToSideAction : AbstractTriggerAction, IResettable
{
    [SerializeField] private float fallingAcceleration;

    private bool _isFalling = false;
    private float _fallingSpeed = 0.0f;

    public float targetAngle;

    private float _offsetToRotation;

    private Quaternion initialRot;

    private float totalAmountFallen = 0f;

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

        if (totalAmountFallen > 90)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,targetAngle + _offsetToRotation));
            return;
        }

        _fallingSpeed -= fallingAcceleration * Time.deltaTime;
        float amountFallen = -_fallingSpeed * Time.deltaTime; 
        totalAmountFallen += amountFallen; 
        transform.Rotate(new Vector3(0, 0, amountFallen));
    }

    public override void CollisionAction()
    {
        _isFalling = true;
    }

    public void ResetAsset()
    {
        _isFalling = false;
        _fallingSpeed = 0;
        totalAmountFallen = 0f;
        transform.rotation = initialRot;
    }
}