using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathGameObject : MonoBehaviour
{
    [SerializeField] private float _duration;

    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    public void DestroyGameObject()
    {
        StartCoroutine(FadeIn(_duration));
    }

    private IEnumerator FadeIn(float duration)
    {
        Color color = _sprite.color;

        for (int i = 0; i < 255; i++)
        {
            color.a = duration - (duration / 255f * i);
            _sprite.color = color;

            yield return null;
        }

        Destroy(gameObject);
    }
}
