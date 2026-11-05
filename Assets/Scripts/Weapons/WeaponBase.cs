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

    [SerializeField] private ProjectilePool projectilePool;

    // readonly : 읽기 전용
    private readonly List<Collider2D> overlapResults = new List<Collider2D>(64);
    private readonly HashSet<IDamageable> hitTargets = new HashSet<IDamageable>();


    protected void InitializeWeapon()
    {
        if (weaponData != null)
        {
            projectilePrefab = weaponData.projectilePrefab;
            projectileSpeed = weaponData.projectileSpeed;
            attackInterval = weaponData.attackInterval;
        }

        if (projectilePool != null && projectilePrefab != null) // 원거리 무기만 체크
            projectilePool.Configure(projectilePrefab.gameObject);
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

        if (projectilePool == null) return false;

        //ProjectileMovement projectileObject = Instantiate(projectilePrefab, 
        //    projectileSpawnPoint.position, Quaternion.identity);

        ProjectileMovement projectileObject
            = projectilePool.Get(projectileSpawnPoint.position);

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
        Attack();
        FinishAttack();
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
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(targetLayers);

        overlapResults.Clear();
        int resultCount // OverlapCircle : Count 반환
            = Physics2D.OverlapCircle(center, radius, contactFilter, overlapResults);

        hitTargets.Clear();

        int damage = GetAttackDamage();

        for (int i = 0; i < resultCount; i++)
        {
            Collider2D hitCollider = overlapResults[i];
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();
            if (damageable == null) continue;

            // HashSet에 포함되어 있으면 반복문 계속
            if (hitTargets.Add(damageable) == false) continue; // 중복 공격 방지
            
            damageable.TakeDamage(damage);
        }

        return hitTargets.Count;
    }
}
