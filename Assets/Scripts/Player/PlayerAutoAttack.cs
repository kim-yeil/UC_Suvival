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

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.isDead == true) return;
        // 공격 처리
        TryAutoAttack();
    }

    /// <summary>
    /// 매 프레임 마다 공격을 시도하는 함수
    /// </summary>
    void TryAutoAttack()
    {
        if (targetFinder == null) return; // PlayerTargetFinder 확인

        if (CanAttack() == false) return; // 공격 가능 시간 확인

        Transform target = targetFinder.GetNearestTarget();
        if (target == null) return; // 공격 타겟 유무 확인

        bool isProjectileCreated = SpawnProjectile(target);
        if (isProjectileCreated == false) return;

        FinishAttack();
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
}
