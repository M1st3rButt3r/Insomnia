using UnityEngine;

public class JumpMushroom : MotherMushroom
{
    public float JumpBoost;

    protected override void StartTopEffect()
    {
        Debug.Log("Applying Effect 'Jump Boost'");
        PlayerController.playerController.rb.velocity = new Vector2(PlayerController.playerController.rb.velocity.x, 0);
        PlayerController.playerController.rb.AddForce(new Vector2(0, JumpBoost));
    }
}
