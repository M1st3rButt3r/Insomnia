using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TriggerSwapStateAction : AbstractTriggerAction
{
    [SerializeField] private Sprite toggleSprite;
    [SerializeField] private bool shouldToggleSprites;

    private Sprite _initialSprite;
    private bool _isChanged;

    public void Awake()
    {
        _initialSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public override void CollisionExit()
    {
        _isChanged = !_isChanged;
        GetComponent<SpriteRenderer>().sprite = _isChanged || !shouldToggleSprites ? toggleSprite: _initialSprite;
        this.GetComponent<Collider2D>().enabled = false;
    }
}
