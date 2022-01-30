using UnityEngine;

public class TranslucentMushroom : MotherMushroom
{
    protected override void StartEffect()
    {
        Debug.Log("Player is now translucent");
        GameManager.Instance.player.layer = 30;
    }

    protected override void EndEffect()
    {
        Debug.Log("Player is not translucent anymore");
        GameManager.Instance.player.layer = 31;
    }
}
