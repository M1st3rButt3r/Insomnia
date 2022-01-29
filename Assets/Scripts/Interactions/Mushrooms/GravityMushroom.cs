using UnityEngine;

public class GravityMushroom : MotherMushroom
{
    protected override void StartEffect()
    {
        Debug.Log("Applying Effect 'Gravity'");
        PlayerController.playerController.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    protected override void EndEffect()
    {
        Debug.Log("Removing Effect 'Gravity'");
        PlayerController.playerController.rb.constraints = RigidbodyConstraints2D.None;
    }
}
