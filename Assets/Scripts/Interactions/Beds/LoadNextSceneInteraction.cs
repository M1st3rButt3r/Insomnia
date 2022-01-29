using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneInteraction : BedInteraction
{
    public string nextScene;
    
    protected override void Interact()
    {
        if (!CanInteract)
        {
            return;
        }
        
        Debug.Log($"Loading Scenes {nextScene}...");
        SceneManager.LoadScene($"{nextScene}");
    }
}