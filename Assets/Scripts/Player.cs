using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _healthBarTemplate;

    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;

    private int _currentHealth;
    private float _dalayShoot;

    private Animator _animator;
    private Collider2D _collider2D;
    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction<Weapon> WeaponChanged;
    public event UnityAction died;


    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentWeapon = _weapons[0];
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();        

        _healthBarTemplate.GetComponentInChildren<PlayerHealthBar>().Init(this);

        Canvas _canvas = Instantiate(_healthBarTemplate, transform.position + new Vector3(0.27f, 1.77f, 0), transform.rotation, transform).GetComponent<Canvas>();        
        _canvas.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Update()
    {
        _dalayShoot += Time.deltaTime;

        if (_dalayShoot <= _currentWeapon.DalayShoot)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _dalayShoot = 0;
            _animator.SetTrigger("Attack");
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    public void ApplyDamege(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        _animator.SetTrigger("Hurt");
        if (_currentHealth <= 0)
        {            
            _animator.SetBool("Death", true);
            _collider2D.isTrigger = false;
            this.GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviewWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {        
        _currentWeapon = weapon;
        Debug.Log(weapon.Label);
        WeaponChanged?.Invoke(weapon);
    }
}
