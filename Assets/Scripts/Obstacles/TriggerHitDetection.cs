using System;
using UnityEngine;

public class TriggerHitDetection : MonoBehaviour
{
    [SerializeField]
    public AbstractTriggerAction abstractTriggerAction;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
        {
            return;
        }

        abstractTriggerAction.TriggerAction();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        abstractTriggerAction.TriggerExit();
    }
}
