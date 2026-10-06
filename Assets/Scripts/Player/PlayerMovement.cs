using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private Rigidbody2D playerRigidbody;
    private Vector2 moveDirection;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerHealth playerHealth;

    private List<Transform> playerTransforms = new List<Transform>(10);
    private float transformSaveTimer;
    private float transformSaveInterval = 0.1f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
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

        //transformSaveTimer+= Time.deltaTime;
        //if (transformSaveTimer >= transformSaveInterval)
        //{
        //    transformSaveTimer = 0;
        //    playerTransforms.RemoveAt(0);
        //    playerTransforms.Add(transform);
        //    Debug.Log(playerTransforms);
        //}
    }

    private void FixedUpdate()
    {
        if (playerHealth.isDead == true)
        {
            playerRigidbody.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 velocity = moveDirection * moveSpeed;
        playerRigidbody.linearVelocity = velocity;
    }
}
