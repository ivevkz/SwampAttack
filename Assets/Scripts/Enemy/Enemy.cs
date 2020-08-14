using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Player _target;
    [SerializeField] private Transform _shootPoint;

    private int _currentHealth;
    private BoxCollider2D _collider2D;

    private Animator _animator;
    public Player Target => _target;
    public Weapon Weapon => _weapon;
    public Transform ShootPoint => _shootPoint;
    public int Reward => _reward;

    public event UnityAction<Enemy> Dying;
    public event UnityAction<int, int> HealthChanged;

    public void Init(Player player)
    {
        _target = player;
        //_target.died += Win;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<BoxCollider2D>();
        _currentHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        _animator.SetBool("Hurt", true);

        if (_currentHealth <= 0)
        {
            _animator.SetBool("Death", true);
            _collider2D.isTrigger = false;
            Dying?.Invoke(this);
        }
    }

    private void Win()
    {
        //_target.died -= Win;
        if (this != null)
        {
            _animator.SetBool("Idle", true);
            _animator.SetBool("Walk", false);
        }
    }

}
