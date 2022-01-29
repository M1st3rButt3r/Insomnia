using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MoveLandscapeLayer : MonoBehaviour
{
    [SerializeField] private float transformValue;

    private Transform _referencePosition;

    private void Start()
    {
        Debug.Log(Camera.main);
        _referencePosition = Camera.main!.transform;
    }

    private void Update()
    {
        Debug.Log(_referencePosition.position);
        Vector3 position = this.transform.position;
        this.transform.position = new Vector3(_referencePosition.position.x * transformValue, position.y, position.z);
    }
}
