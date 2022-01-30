using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabenSpawner : MonoBehaviour
{
    public GameObject Rabe;
    public float minTimeRange = 0.5f;
    public float maxTimeRange = 3f;
    private float _cooldown;
    private float _maxCooldown = 2;

    public float minforWard = 1;
    public float maxforWard = 3;
    public float minupWard = 0.1f;
    public float maxupWard = 0.3f;

    public float lifeTime = 10f;
    void Update()
    {
        if(_cooldown > _maxCooldown){
            Instantiate(Rabe);
            _cooldown = 0;
            _maxCooldown = Random.Range(minTimeRange, maxTimeRange);
        }
        _cooldown += Time.deltaTime;
    }
}
