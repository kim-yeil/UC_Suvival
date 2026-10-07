using UnityEngine;

public class PlayerTargetFinder : MonoBehaviour
{
    [SerializeField] private float targetRange = 5.0f;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private float expGainRange = 1.5f;
    [SerializeField] private LayerMask expLayer;

    private Transform nearestTarget;
    private EnemyHealth enemyHealth;

    // Update is called once per frame
    void Update()
    {
        // 가장 가까운 적 탐색
        FingNearestTarget();

        // 선 긋기
        DrawTargetLine();

        // EXP 탐색
        FindNearExpGem();
    }

    void FindNearExpGem()
    {
        Collider2D[] expGemsInRnage = 
            Physics2D.OverlapCircleAll(transform.position, expGainRange, expLayer);

        for (int i = 0; i < expGemsInRnage.Length; i++)
        {
            ExpGem expGem = expGemsInRnage[i].GetComponent<ExpGem>();
            if (expGem == null) return;

            expGem.SetTargetToPlayer(transform);
            expGem.isTargeted = true;
        }
    }

    void FingNearestTarget()
    {
        // 반경 내의 기준 Layer를 가진 타겟 탐색
        Collider2D[] targetsInRange =
            Physics2D.OverlapCircleAll(transform.position, targetRange, enemyLayer);

        nearestTarget = null;
        float nearestDistance = 256.0f;

        for (int i = 0; i < targetsInRange.Length; i++)
        {
            Collider2D targetCollider = targetsInRange[i];
            EnemyHealth targetHealth = targetCollider.GetComponent<EnemyHealth>();
            if (targetHealth == null) continue;
            if (targetHealth.isDead == true) continue;

            Vector2 playerPosition = transform.position;
            Vector2 targetPosition = targetCollider.transform.position;

            float distance = Vector2.Distance(playerPosition, targetPosition);

            if (distance < nearestDistance)
            {
                nearestTarget = targetCollider.transform;
                nearestDistance = distance;
            }
        }
    }

    void DrawTargetLine()
    {
        if (nearestTarget == null) return;

        Debug.DrawLine(transform.position, nearestTarget.position, Color.red);
    }

    /// <summary>
    /// 가장 가까운 대상의 Transform 정보를 반환하는 함수
    /// </summary>
    /// <returns> 가장 가까운 대상의 Transform 정보 </returns>
    public Transform GetNearestTarget()
    {
        return nearestTarget;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
