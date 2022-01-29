using UnityEngine;

public class TriggerHitDetection : MonoBehaviour
{
    [SerializeField]
    public AbstractTriggerAction abstractTriggerAction;

    private void ColliderEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Triggered");
        abstractTriggerAction.TriggerAction();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Triggered");
        abstractTriggerAction.TriggerAction();
    }
}
