using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _distance;
    [SerializeField] private float _rangeSpred;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _distance += Random.Range(-_rangeSpred, _rangeSpred);
    }

    private void Update()
    {
        if (Target == null)
            return;               

        if (Vector2.Distance(transform.position, Target.transform.position) < _distance)
        {
            _animator.SetBool("Walk", false);
            NeedTransit = true;
        }
    }
}
