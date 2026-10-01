using UnityEngine;

public class PlayerTargetFinder : MonoBehaviour
{
    [SerializeField] private float targetRange = 5.0f;
    [SerializeField] private LayerMask enemyLayer;

    private Transform nearestTarget;
    private EnemyHealth enemyHealth;

    private float targetFindTimer;
    private string targetName;
    private float targetDistance;

    // Update is called once per frame
    void Update()
    {
        // 가장 가까운 적 탐색
        FingNearestTarget();

        // 선 긋기
        DrawTargetLine();

        PrintTarget();
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
                targetName = targetCollider.gameObject.name;
                nearestDistance = distance;
                targetDistance = distance;
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

    private void PrintTarget()
    {
        targetFindTimer += Time.deltaTime;

        if (targetFindTimer < 1) return;

        targetFindTimer = 0;
        if (nearestTarget == null) Debug.Log("No Target");
        else Debug.Log("가장 가까운 적 : " + targetName + " / 거리 : " + targetDistance);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
