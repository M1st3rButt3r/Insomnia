using UnityEngine;

public class MoveLandscapeLayer : MonoBehaviour
{
    [SerializeField] private float transformValue;

    private Transform _referencePosition;

    private void Start()
    {
        _referencePosition = Camera.main!.transform;
    }

    private void Update()
    {
        Vector3 position = this.transform.position;
        this.transform.position = new Vector3(_referencePosition.position.x * transformValue - 17, position.y, position.z);
    }
}
