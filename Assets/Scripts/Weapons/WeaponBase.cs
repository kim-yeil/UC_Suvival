using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponBase : MonoBehaviour
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
}
