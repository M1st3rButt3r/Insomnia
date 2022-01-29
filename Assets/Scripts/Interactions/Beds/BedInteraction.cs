using UnityEngine;

public class BedInteraction : MonoBehaviour
{
    private bool CanInteract;

    private void Start()
    {
        PlayerInput.Instance.Interact += Interact;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        CanInteract = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        CanInteract = false;
    }

    private void Interact()
    {
        if (! CanInteract)
        {
            return;
        }
        GameManager.Instance.Finish();
    }
}
