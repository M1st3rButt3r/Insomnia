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
