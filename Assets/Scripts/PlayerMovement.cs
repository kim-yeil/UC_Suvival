using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private Rigidbody2D playerRigidbody;
    private Vector2 moveDirection;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;

        if (moveDirection.magnitude != 0)
        {
            animator.SetBool("IsMoving", true);
            if (horizontal == 1)
            {
                spriteRenderer.flipX = false;
            }
            else if (horizontal == -1)
            {
                spriteRenderer.flipX = true;
            }
        }
        else animator.SetBool("IsMoving", false);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = moveDirection * moveSpeed;
        playerRigidbody.linearVelocity = velocity;

    }
}
