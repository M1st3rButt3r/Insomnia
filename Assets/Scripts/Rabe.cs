using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabe : MonoBehaviour
{
    public float minforWard = 1;
    public float maxforWard = 3;
    public float minupWard = 0.1f;
    public float maxupWard = 0.3f;
    void Update()
    {
        transform.position += new Vector3(Random.Range(minforWard, maxforWard)*Time.deltaTime, Random.Range(minupWard, maxupWard)*Time.deltaTime, 0);
    }
}
