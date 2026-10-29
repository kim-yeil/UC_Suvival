using UnityEngine;
using System.Collections.Generic;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] ProjectileMovement projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] protected float projectileSpeed = 5.0f;
    [SerializeField] private float attackInterval = 0.7f;

    protected int bonusDamage;
    private float nextAttackTime;

    [SerializeField] private int succededCount = 0;

    protected void InitializeWeapon()
    {
        if (weaponData != null)
        {
            projectilePrefab = weaponData.projectilePrefab;
            projectileSpeed = weaponData.projectileSpeed;
            attackInterval = weaponData.attackInterval;
        }
    }

    protected WeaponDataSO GetWeaponData()
    {
        return weaponData;
    }

    protected Vector2 GetSpawnPosition()
    {
        return projectileSpawnPoint.position;
    }

    protected bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }

    protected void FinishAttack()
    {
        nextAttackTime = Time.time + attackInterval;
    }

    protected bool FireProjectile(Vector2 direction)
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("투사체 Prefab 없음!");
            return false;
        }

        ProjectileMovement projectileObject = Instantiate(projectilePrefab, 
            projectileSpawnPoint.position, Quaternion.identity);

        if (projectileObject == null) return false;
        projectileObject.Initialized(direction, projectileSpeed);
        
        if (weaponData != null) projectileObject.SetBaseDamage(weaponData.baseDamage);
        projectileObject.DamageIncrease(bonusDamage);

        return true;
    }
    public void IncreaseDamage(int amount)
    {
        bonusDamage += amount;
    }

    public void IncreaseAttackSpeed(float amount)
    {
        attackInterval -= amount;
        if (attackInterval < 0.15)
        {
            attackInterval = 0.15f;
            Debug.Log("공격속도 MAX");
        }
    }

    public void TryAttack()
    {
        if (CanAttack() != true) return;
        if (Attack() == true) FinishAttack();
    }

    // abstract : 상속 받는 클래스가 정의를 해야함
    protected abstract bool Attack();

    protected int GetAttackDamage()
    {
        int baseDamage = 1; // 무기 데이터 지정 없을 시 default 값
        if (weaponData != null) baseDamage = weaponData.baseDamage;

        return Mathf.Max(1, baseDamage + bonusDamage);
    }

    protected int DamageTargetsInCircle(Vector2 center, float radius, LayerMask targetLayers)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius, targetLayers);

        List<IDamageable> hitTargets = new List<IDamageable>();
        int damage = GetAttackDamage();

        foreach(Collider2D hitCollider in colliders)
        {
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();
            if (damageable == null) continue;
            if (hitTargets.Contains(damageable) == true) continue; // 중복 공격 방지

            hitTargets.Add(damageable);
            damageable.TakeDamage(damage);
        }

        return hitTargets.Count;
    }
}
