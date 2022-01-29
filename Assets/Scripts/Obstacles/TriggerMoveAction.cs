using UnityEngine;

public class TriggerMoveAction : AbstractTriggerAction
{
    [SerializeField] private Vector3 moveToPosition;
    [SerializeField] private float speed;
    [SerializeField] private bool spins;

    private bool _theySeeMeRolling;

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
            transform.Rotate(new Vector3(0, 0,speed * Time.deltaTime * 180 / Mathf.PI * GetComponent<CircleCollider2D>().radius));
        }
    }

    public override void TriggerAction()
    {
        _theySeeMeRolling = true;
    }
}