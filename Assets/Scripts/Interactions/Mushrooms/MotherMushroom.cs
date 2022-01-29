using System.Collections.Generic;
using UnityEngine;

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
        if (_interactingSince == null)
        {
            return;
        }

        if (_interactingSince + interactionTime >= Time.time)
        {
            ActiveDayTime currentTime = GameManager.Instance.secondRun ? ActiveDayTime.Day : ActiveDayTime.Night;
            if (activeDayTime == ActiveDayTime.DayAndNight || activeDayTime == currentTime)
                UpdateEffect();
            else if (dieOnWrongDayTime)
                GameManager.Instance.Die("You ate the mushroom at the wrong time!");
            return;
        }

        _interactingSince = null;
        EndEffect();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _interactingSince = Time.time;
            
            if (destroyOnEffect)
            {
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(GetComponent<SpriteRenderer>());
            }

            StartEffect();
        }
    }

    public static void ResetAll()
    {
        foreach (MotherMushroom mushroom in _mushrooms)
        {
            Instantiate(mushroom.gameObject);
            Destroy(mushroom.gameObject);
        }
    }

    protected virtual void UpdateEffect() {}

    protected virtual void StartEffect() {}

    protected virtual void EndEffect() {}
}
