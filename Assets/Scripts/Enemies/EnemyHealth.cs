using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    public bool isDead;
    private Animator animator;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float hitFeedbackInterval = 0.3f;
    [SerializeField] private float hitFeedbackTimer;
    [SerializeField] private Color hitColor = Color.white;
    private Color originColor;
    private bool isHit;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        isHit = false;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (isHit == false) return;

        hitFeedbackTimer += Time.deltaTime;
        if (hitFeedbackTimer >= hitFeedbackInterval)
        {
            isHit = false;
            spriteRenderer.color = originColor;
        }
    }

    /// <summary>
    /// 데미지를 적용하는 함수
    /// </summary>
    /// <param name="damageAmount"> 적용할 데미지 양 </param>
    public void TakeDamage(int damageAmount)
    {
        if (isDead == true) return;

        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            // 사망 처리
            Die();
        }
        else HitFeedback();
    }

    /// <summary>
    /// 사망 처리 함수
    /// </summary>
    void Die()
    {
        isDead = true;
        animator.SetTrigger("DIE");
    }

    public void DeathAnimationEnd()
    {
        Destroy(gameObject);
    }

    void HitFeedback()
    {
        isHit = true;
        spriteRenderer.color = hitColor;
        hitFeedbackTimer = 0;
    }
}
