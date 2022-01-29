using System;
using UnityEngine;

public class GravityMushroom : MotherMushroom
{
    public bool destroyOnEffect;
    
    protected override void StartEffect()
    {
        Debug.Log("Applying Effect 'Gravity'");
        PlayerController.playerController.rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        if (destroyOnEffect)
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<SpriteRenderer>());
        }
    }

    protected override void EndEffect()
    {
        Debug.Log("Removing Effect 'Gravity'");
        PlayerController.playerController.rb.constraints = RigidbodyConstraints2D.None;

        if (destroyOnEffect)
            Destroy(gameObject);
    }
}
