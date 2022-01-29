using UnityEngine;

public class QuitGameInteraction : BedInteraction
{
    protected override void Interact()
    {
        if (!CanInteract)
        {
            return;
        }
        
        Debug.Log("Quit Bed interaction: Quitting the game");
        
        // Quits finished Application
        Application.Quit(0);
        
        // Quits the play mode
        UnityEditor.EditorApplication.isPlaying = false;
    }
}