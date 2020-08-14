using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    public event UnityAction<bool> HitEnemy;

    public void Update()
    {
        if (transform.rotation == Quaternion.Euler(0, 0, 90))        
            transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
        else
            transform.Translate(Vector2.right * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.TryGetComponent(out Enemy enemy) && transform.rotation == Quaternion.Euler(0, 0, 90))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }else
        if (collision.gameObject.TryGetComponent(out Player player) && transform.rotation == Quaternion.Euler(0, 0, -90))
        {
            player.ApplyDamege(_damage);
            Destroy(gameObject);
        }else
        if (collision.gameObject.TryGetComponent(out DestroyBullet destroyBullet))
        {
            Destroy(gameObject);
        }
    }
}
