using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _speed;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Target == null)
            return;

        if (!_animator.GetBool("Hurt") && !_animator.GetBool("Death"))
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, 0 * Time.deltaTime);
    }
}
