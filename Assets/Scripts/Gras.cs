using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gras : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        _animator.SetInteger("Random",Mathf.RoundToInt(Random.Range(0, 2)));
        _animator.SetTrigger("Trigger");
    }
}
