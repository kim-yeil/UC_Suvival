using System.Xml.Schema;
using UnityEditor;
using UnityEngine;

public class MeleeEnemyChaser : MonoBehaviour
{
    [SerializeField] private Transform playerTransfomrm;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float stopDistance = 2.2f;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void FixedUpdate()
    {
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
}
