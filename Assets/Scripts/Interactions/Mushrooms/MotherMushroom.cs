using UnityEngine;

public class MotherMushroom : MonoBehaviour
{
    public float interactionTime;

    private float? _interactingSince;

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
            StartEffect();
        }
    }

    protected virtual void UpdateEffect() {}

    protected virtual void StartEffect() {}

    protected virtual void EndEffect() {}
}
