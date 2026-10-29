using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CircleAutoAttack : WeaponBase
{
    [SerializeField] private Animator animator;

    private LayerMask targetLayers;
    private float radius = 1.2f;

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        InitializeWeapon();
    }

    protected override bool Attack()
    {
        DamageTargetsInCircle(transform.position, radius, targetLayers);
        return true;
    }
}
