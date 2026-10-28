using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 플레이어의 자동 공격을 담당하는 Class
/// </summary>
public class PlayerAutoAttack : WeaponBase // WeaponBase를 상속받음
{
    [SerializeField] private PlayerTargetFinder targetFinder;
    [SerializeField] private int projectileCount;
    [SerializeField] private float projectileSpreadAngle;
    [SerializeField] private float spreadStartAngle;
        
    private PlayerHealth playerHealth;

    private void Awake()
    {
        InitializeWeapon();

        // 투사체 개수, 각도 적용
        ApplySpreadSettings();

        playerHealth = GetComponent<PlayerHealth>();
        if (targetFinder == null) targetFinder = GetComponent<PlayerTargetFinder>();
    }

    bool SpawnProjectile(Transform target)
    {
        Vector2 startposition = GetSpawnPosition();
        Vector2 targetPosition = target.position;
        Vector2 direction = targetPosition - startposition;
        if (direction == Vector2.zero) return false;

        bool createAny = false; // 반환용 값

        spreadStartAngle = projectileCount * projectileSpreadAngle;

        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 shotDirection =
                Quaternion.Euler(0, 0, spreadStartAngle - (i + 1) * projectileSpreadAngle) * direction;

            if (FireProjectile(shotDirection) == true) createAny = true;
        }

        return createAny;
    }

    
    public void ProjectileSpeedIncrease(float amount)
    {
        projectileSpeed += amount;
    }

    public void IncreaseSpreadProjectile(int amount)
    {
        projectileCount += amount;
        if (projectileCount > 5) projectileCount = 5;
    }

    void ApplySpreadSettings()
    {
        WeaponDataSO data = GetWeaponData();
        projectileCount = data.projectileCount;
        projectileSpreadAngle = data.spreadAngle;
    }

    protected override bool Attack()
    {
        if (targetFinder == null) return false;

        Transform target = targetFinder.GetNearestTarget();
        if (target == null) return false;

        return SpawnProjectile(target);
    }
}
