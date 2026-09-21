using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private Transform playerTransfomrm;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance = 0.8f;

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
        Vector2 velocity = direction * moveSpeed;

        body.linearVelocity = velocity;
    }

    public void SetTarget(Transform targetTransform)
    {
        playerTransfomrm = targetTransform;
    }

    public void SetVelocity(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        Debug.Log("¼Óµµ : " + moveSpeed);
    }
}
