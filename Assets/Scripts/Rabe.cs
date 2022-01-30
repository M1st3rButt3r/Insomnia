using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabe : MonoBehaviour
{
    public RabenSpawner spawnerGO;
    void Start() {
        Destroy(this.gameObject, spawnerGO.lifeTime);
    }
    void Update()
    {
        transform.position += new Vector3(Random.Range(spawnerGO.minforWard, spawnerGO.maxforWard)*Time.deltaTime, Random.Range(spawnerGO.minupWard, spawnerGO.maxupWard)*Time.deltaTime, 0);
    }
}
