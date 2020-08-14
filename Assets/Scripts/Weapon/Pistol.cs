using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private bool _isEnemy;
    public override void Shoot(Transform shootPoint)
    {
        if (_isEnemy)        
           Instantiate(Bullet, shootPoint.position, Quaternion.Euler(0, 0, -90));
        else
            Instantiate(Bullet, shootPoint.position, Quaternion.Euler(0, 0, 90));
    }
}
