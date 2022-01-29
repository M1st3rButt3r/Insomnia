using UnityEngine;

public class TranslucentMushroom : MotherMushroom
{
    public GameObject player;
    
    protected override void StartEffect()
    {
        Debug.Log("Player is now translucent");
        player.layer = 30;
    }

    protected override void EndEffect()
    {
        Debug.Log("Player is not translucent anymore");
        player.layer = 31;
    }
}
