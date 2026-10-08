using UnityEngine;
using UnityEngine.Lumin;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text playerHPText;

    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;

    [SerializeField] private Image playerHP;

    public bool isDead;
    public bool isInvincible;
    private float invincibleDuration = 1.0f;
    private float invincibleTimer;

    private Animator playerAnimator;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        isInvincible = false;
        playerAnimator = GetComponent<Animator>();
        UpdatePlayerHPUI();
    }

    private void Update()
    {
        if (isInvincible == true)
        {
            invincibleTimer += Time.deltaTime;
            if (invincibleTimer >= invincibleDuration)
            {
                isInvincible = false;
                invincibleTimer = 0.0f;
            }
        }
    }

    public void GetHpRecoverItem(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth) currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead == true) return;
        if (isInvincible == true) return;

        isInvincible = true;
        currentHealth -= damageAmount;
        UpdatePlayerHPUI();

        if (currentHealth <= 0)
        {
            Die(); //사망처리
        }
    }

    void UpdatePlayerHPUI()
    {
        if (playerHP == null) return;

        // C# 에서 정수/정수 = 정수이므로, float로 형변환 필요
        playerHP.fillAmount = (float)currentHealth / (float)maxHealth;
        playerHPText.text = "HP : " + currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    void Die()
    {
        isDead = true;
        playerAnimator.SetTrigger("Die");
    }
}
