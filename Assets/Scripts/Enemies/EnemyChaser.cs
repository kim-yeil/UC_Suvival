using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private Transform playerTransfomrm;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance = 0.3f;

    [SerializeField] private float separationRadius = 0.3f;
    [SerializeField] private float separationWeight = 0.4f;
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (playerTransfomrm == null)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 playerPosition = playerTransfomrm.position;
        Vector2 enemyPosition = transform.position;

        Vector2 difference = playerPosition - enemyPosition;

        float distance = difference.magnitude;

        spriteRenderer.flipX = difference.x < 0;

        if (distance <= stopDistance)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = difference.normalized;
        // 분리 방향 계산
        Vector2 separationDirection = CalculateSeparationDirection(enemyPosition);
        Vector2 finalDirection = direction + (separationDirection * separationWeight);
        if (finalDirection != Vector2.zero) finalDirection = finalDirection.normalized;

        body.linearVelocity = finalDirection * moveSpeed;
    }

    public void SetTarget(Transform targetTransform)
    {
        playerTransfomrm = targetTransform;
    }

    public void SetVelocity(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    Vector2 CalculateSeparationDirection(Vector2 enemyPosition)
    {
        Collider2D[] nearbyEnemies
            = Physics2D.OverlapCircleAll(enemyPosition, separationRadius, enemyLayer);

        Vector2 separationDirection = Vector2.zero;

        for (int i = 0; i<nearbyEnemies.Length; i++)
        {
            Collider2D nearbyEnemy = nearbyEnemies[i];
            if (nearbyEnemy.transform == transform) continue;

            Vector2 nearbyPosition = nearbyEnemy.transform.position;
            Vector2 awayDirection = enemyPosition - nearbyPosition;
            float distance = awayDirection.magnitude;
            if (distance <= 0.0f) continue;

            separationDirection += awayDirection.normalized / distance;
        }

        return separationDirection;
    }
}
