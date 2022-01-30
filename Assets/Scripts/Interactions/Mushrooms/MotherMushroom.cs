using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public enum ActiveSides
{
    All,
    Top
}

public enum ActiveDayTime
{
    DayAndNight,
    Day,
    Night
}

public class MotherMushroom : MonoBehaviour, IResettable
{
    public float interactionTime;
    public ActiveDayTime activeDayTime;
    public ActiveSides activeSide;
    public bool dieOnWrongDayTime;
    public bool destroyOnEffect;

    private float? _interactingSince;
    private Collider2D _playerCollider;

    private void Start()
    {
        GameManager.Instance.AddToResettables(this);
    }

    private void FixedUpdate()
    {
        if (_interactingSince == null || _interactingSince + interactionTime <= Time.time)
        {
            return;
        }

        _interactingSince = null;
        EndEffect();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        ActiveDayTime currentTime = GameManager.Instance.secondRun ? ActiveDayTime.Night : ActiveDayTime.Day;
        if (activeDayTime != ActiveDayTime.DayAndNight && activeDayTime != currentTime && dieOnWrongDayTime)
        {
            GameManager.Instance.Die("You ate the mushroom at the wrong time!");
        }

        _interactingSince = Time.time;
 
        if (activeSide == ActiveSides.Top)
        {
            if (other.gameObject.transform.position.y >
                gameObject.transform.position.y + (gameObject.transform.localScale.y / 2))
            {
                StartTopEffect(other);
                
                // Destroys mushroom in "top-only mode"
                if (!destroyOnEffect) return;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            
            return;
        }

        StartEffect();
        
        // Destroys mushroom in "normal mode"
        if (!destroyOnEffect) return;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ResetAsset()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    protected virtual void StartEffect() {}

    protected virtual void StartTopEffect(Collider2D other) {}

    protected virtual void EndEffect() {}
}
