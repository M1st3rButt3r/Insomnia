using System;
using UnityEngine;

public class BedInteraction : MonoBehaviour
{
    public static BedInteraction Instance;
    
    private bool CanInteract;

    public int playersInCollider;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerInput.Instance.Interact += Interact;
        PlayerInput.Instance.Restart += () =>
        {
            playersInCollider = 0;
        };
        
        PlayerInput.Instance.RestartHold += () =>
        {
            playersInCollider = 0;
        };
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        CanInteract = true;
        playersInCollider += 1;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        CanInteract = false;
        playersInCollider -= 1;
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
