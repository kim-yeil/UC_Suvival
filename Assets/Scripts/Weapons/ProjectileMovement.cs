using UnityEngine;

public enum DestyorType
{
    byTime,
    byDistance
}

/// <summary>
/// 투사체의 이동을 담당하는 클래스
/// </summary>
public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2.0f;

    [SerializeField] private Rigidbody2D body;

    private Vector2 moveDirection;
    private float moveSpeed;
    private bool isInitilaized = false;

    [SerializeField] private int damageAmount = 1;

    [SerializeField] private DestyorType destroyType;
    private float movedDistance = 0.0f;
    private float maxMoveDistance = 8.0f;

    private void Reset()
    {
        if (body == null) body = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (destroyType == DestyorType.byTime)
        {
            Invoke("PrintMovedDistance", lifeTime);
            Destroy(gameObject, lifeTime);
        }
    }

    void PrintMovedDistance()
    {
        Debug.Log(movedDistance);
    }

    /// <summary>
    /// prefab 생성 후 초기화하는 함수
    /// </summary>
    /// <param name="direction"> 투사체의 진행 방향 </param>
    /// <param name="speed"> 투사체의 이동 속도 </param>
    public void Initialized(Vector2 direction, float speed)
    {
        moveDirection = direction.normalized;
        moveSpeed = speed;

        isInitilaized = true;
    }

    /// <summary>
    /// 투사체의 방향과 속도가 정확히 적용 된 후에 투사체를 이동시킴
    /// </summary>
    private void FixedUpdate()
    {
        if (isInitilaized == false) return;

        body.linearVelocity = moveDirection * moveSpeed;
        
        movedDistance += moveSpeed * Time.fixedDeltaTime;
        if (destroyType == DestyorType.byDistance && movedDistance >= maxMoveDistance)
        {
            Debug.Log(movedDistance);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") == true)
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null) enemyHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
