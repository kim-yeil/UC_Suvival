using UnityEngine;

/// <summary>
/// 플레이어의 자동 공격을 담당하는 Class
/// </summary>
public class PlayerAutoAttack : MonoBehaviour
{
    [SerializeField] private PlayerTargetFinder targetFinder;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float attackInterval = 0.7f;

    private float nextAttackTime;

    private void Awake()
    {
        if (targetFinder == null) targetFinder = GetComponent<PlayerTargetFinder>();
    }

    // Update is called once per frame
    void Update()
    {
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

        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null) // 적 체력 스크립트 유무 확인
        {
            enemyHealth.TakeDamage(damageAmount);
            nextAttackTime = Time.time + attackInterval;
        }
    }
}
