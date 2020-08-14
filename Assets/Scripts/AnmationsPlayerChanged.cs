using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnmationsPlayerChanged : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.WeaponChanged += ChengeAnimator;
    }

    private void OnDisable()
    {
        _player.WeaponChanged -= ChengeAnimator;
    }

    private void ChengeAnimator(Weapon weapon)
    {
        if (weapon.Label == "Pistol")
            _animator.SetLayerWeight(1, 0);
        else
        if (weapon.Label == "Machine")
            _animator.SetLayerWeight(1, 1);
        else
        if (weapon.Label == "Gun")
            _animator.SetLayerWeight(2, 1);
    }
}
