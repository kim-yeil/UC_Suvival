using UnityEngine;

/// <summary>
/// 플레이어의 자동 공격을 담당하는 Class
/// </summary>
public class PlayerAutoAttack : MonoBehaviour
{
    [SerializeField] private PlayerTargetFinder targetFinder;
    [SerializeField] private float attackInterval = 0.7f;
    [SerializeField] ProjectileMovement projectilePrefab;

    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed = 5.0f;

    private float nextAttackTime;
    private PlayerHealth playerHealth;

    private void Awake()
    {
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

        if (Time.time < nextAttackTime) return; // 공격 가능 시간 확인

        Transform target = targetFinder.GetNearestTarget();
        if (target == null) return; // 공격 타겟 유무 확인

        bool isProjectileCreated = SpawnProjectile(target);
        if (isProjectileCreated == false) return;

        nextAttackTime = Time.time + attackInterval;
    }

    bool SpawnProjectile(Transform target)
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("투사체 Prefab 없음!");
            return false;
        }

        Vector2 startPosition = projectileSpawnPoint.position;
        Vector2 targetPosition = target.position;
        Vector2 direction = targetPosition - startPosition;

        if (direction == Vector2.zero) return false; // 타겟과 발사 위치가 동일한 경우

        ProjectileMovement projectileObject = 
            Instantiate(projectilePrefab, startPosition, Quaternion.LookRotation(Vector3.forward, direction));

        if (projectileObject != null)
        {
            projectileObject.Initialized(direction.normalized, projectileSpeed);
            return true;
        }

        return false;
    }
}
