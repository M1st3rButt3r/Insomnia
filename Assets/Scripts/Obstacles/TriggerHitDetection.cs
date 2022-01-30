using UnityEngine;

public class TriggerHitDetection : MonoBehaviour
{
    [SerializeField]
    public AbstractTriggerAction abstractTriggerAction;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Player"))
        {
            return;
        }
        abstractTriggerAction.CollisionAction();
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        abstractTriggerAction.CollisionExit(col);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        abstractTriggerAction.TriggerExit();
    }
}
