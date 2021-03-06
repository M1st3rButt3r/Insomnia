using System;
using UnityEngine;

public class TriggerSwapStateAction : AbstractTriggerAction, IResettable
{
    [SerializeField] private Sprite toggleSprite;
    [SerializeField] private bool shouldToggleSprites;

    private Sprite _initialSprite;
    private bool _isChanged;

    public void Start()
    {
        _initialSprite = GetComponent<SpriteRenderer>().sprite;
        GameManager.Instance.AddToResettables(this);
    }

    public override void CollisionExit(Collision2D _)
    {
        _isChanged = !_isChanged;
        GetComponent<SpriteRenderer>().sprite = _isChanged || !shouldToggleSprites ? toggleSprite: _initialSprite;
        this.GetComponent<Collider2D>().enabled = false;
    }

    public void ResetAsset()
    {
        _isChanged = false;
        GetComponent<SpriteRenderer>().sprite = _initialSprite;
        this.GetComponent<Collider2D>().enabled = true;
    }
}
