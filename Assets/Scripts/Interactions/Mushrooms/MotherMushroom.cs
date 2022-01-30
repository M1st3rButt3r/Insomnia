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

public class MotherMushroom : MonoBehaviour
{
    public float interactionTime;
    public ActiveDayTime activeDayTime;
    public ActiveSides activeSide;
    public bool dieOnWrongDayTime;
    public bool destroyOnEffect;

    private float? _interactingSince;
    private Collider2D _playerCollider;
    private static List<MotherMushroom> _mushrooms = new List<MotherMushroom>();

    private void Start()
    {
        _mushrooms.Add(this);
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
                StartTopEffect();
                
                // Destroys mushroom in "top-only mode"
                if (!destroyOnEffect) return;
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(GetComponent<SpriteRenderer>());
                _mushrooms.Remove(this);
            }
            
            return;
        }

        StartEffect();
        
        // Destroys mushroom in "normal mode"
        if (!destroyOnEffect) return;
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<SpriteRenderer>());
        _mushrooms.Remove(this);
    }

    public static void ResetAll()
    {
        foreach (MotherMushroom mushroom in _mushrooms)
        {
            Instantiate(mushroom.gameObject);
            Destroy(mushroom.gameObject);
        }
    }

    protected virtual void StartEffect() {}

    protected virtual void StartTopEffect() {}

    protected virtual void EndEffect() {}
}
