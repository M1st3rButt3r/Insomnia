using System;
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

    public override void TriggerAction()
    {
        _isChanged = !_isChanged;
        GetComponent<SpriteRenderer>().sprite = _isChanged || !shouldToggleSprites ? toggleSprite: _initialSprite;
    }
}
