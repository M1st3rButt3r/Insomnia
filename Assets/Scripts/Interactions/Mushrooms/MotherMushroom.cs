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
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        _interactingSince = Time.time;

        if (destroyOnEffect)
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<SpriteRenderer>());
        }

        if ((activeSide == ActiveSides.Top) &&
            (other.gameObject.transform.position.y > gameObject.transform.position.y +
                (gameObject.transform.localScale.y / 2)))
        {
            StartTopEffect();
            return;
        }

        StartEffect();
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

    protected virtual void StartTopEffect() {}

    protected virtual void EndEffect() {}
}
