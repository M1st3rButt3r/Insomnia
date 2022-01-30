using UnityEngine;

public class JumpMushroom : MotherMushroom
{
    public float JumpBoost;

    protected override void StartTopEffect()
    {
        GameObject go = GameManager.Instance.secondRun ?
            GameManager.Instance.secondPlayer : GameManager.Instance.player;
        
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        Debug.Log($"Applying Effect 'Jump Boost' to: {go.name}");
        
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, JumpBoost));
    }
}
