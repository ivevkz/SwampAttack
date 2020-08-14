using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private float _dalayShoot;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] protected Bullet Bullet;

    public string Label => _label;
    public Sprite Icon => _icon;
    public int Price => _price;
    public float DalayShoot => _dalayShoot;
    public bool IsBuyed => _isBuyed;

    public abstract void Shoot(Transform shootPoint);

    public void Buyed()
    {
        _isBuyed = true;
    }
}
