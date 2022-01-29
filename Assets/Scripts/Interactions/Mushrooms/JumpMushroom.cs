using UnityEngine;

public class JumpMushroom : MotherMushroom
{
    private float _oldGravityScale;

    protected override void StartEffect()
    {
        // TODO make only boost when jumping upwards
        Debug.Log("Applying Effect 'Jump Boost'");
        _oldGravityScale = PlayerController.playerController.rb.gravityScale;
        PlayerController.playerController.rb.gravityScale = 0f;
    }

    protected override void EndEffect()
    {
        Debug.Log("Removing Effect 'Jump Boost'");
        PlayerController.playerController.rb.gravityScale = _oldGravityScale;
    }
}
