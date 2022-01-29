using System.Collections.Generic;
using UnityEngine;

public class MotherMushroom : MonoBehaviour
{
    public float interactionTime;
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
            UpdateEffect();
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
