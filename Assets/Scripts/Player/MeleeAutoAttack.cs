using NUnit.Framework.Constraints;
using UnityEngine;

public class MeleeAutoAttack : WeaponBase
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerTargetFinder targetFinder;
    [SerializeField] private float forwardDistance = 0.8f;
    [SerializeField] private float hitRadius = 0.85f;
    [SerializeField] private LayerMask targetLayers;

    private Vector2 attackDirection = Vector2.right;

    private void Reset()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Awake()
    {
        InitializeWeapon();
    }
    
    protected override bool Attack()
    {
        Transform target = targetFinder.GetNearestTarget();
        if (target == null) return false;

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        attackDirection = direction.normalized;

        animator.SetTrigger("Atk1");

        return true;
    }

    public void OnMeleeHit()
    {
        Vector2 hitCenter = (Vector2)transform.position + (attackDirection * forwardDistance);

        // 공격 판정
        DamageTargetsInCircle(hitCenter, hitRadius, targetLayers);
    }
}
