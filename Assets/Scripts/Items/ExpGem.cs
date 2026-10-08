using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ExpGem : MonoBehaviour
{
    private int expAmount;
    private Transform playerTransform;
    private Rigidbody2D body;
    public bool isTargeted;
    private float moveSpeed = 4.0f;

    public void Initialize(int amount)
    {
        expAmount = amount;
        isTargeted = false;
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerTransform == null || isTargeted == false)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }
        
        Vector2 playerPosition = playerTransform.position;
        Vector2 gemPosition = transform.position;

        Vector2 difference = playerPosition - gemPosition;
        Vector2 direction = difference.normalized;

        body.linearVelocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerExperience playerExperience = collision.GetComponent<PlayerExperience>();
        if (playerExperience == null) return;
        playerExperience.AddEXP(expAmount);

        Destroy(gameObject);
    }

    public void SetTargetToPlayer(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
}
