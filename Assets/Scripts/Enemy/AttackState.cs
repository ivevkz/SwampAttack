using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State 
{
    [SerializeField] private float _delay;

    private float _lastAttackTime;
    private Weapon _weapon;
    private Transform _shootPoint;
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponent<Enemy>().Weapon;
        _shootPoint = GetComponent<Enemy>().ShootPoint;
    }

    private void Update()
    {
        if (_animator.GetBool("Death"))
            return;
        
        if (_lastAttackTime <= 0)
        {
            _animator.SetBool("Idle", false);            
            _lastAttackTime = _delay;
            Attack();
        }
        else
        {
            _animator.SetBool("Idle", true);
        }        
        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack()    
    {             
        _animator.SetTrigger("Attack");        
        _weapon.Shoot(_shootPoint);
    }
}
