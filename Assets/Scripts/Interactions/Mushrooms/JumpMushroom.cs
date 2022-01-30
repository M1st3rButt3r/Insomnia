using UnityEngine;

public class JumpMushroom : MotherMushroom
{
    public float JumpBoost;

    protected override void StartTopEffect(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        Debug.Log($"Applying Effect 'Jump Boost' to: {other.name}");
        
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, JumpBoost));
    }
}
