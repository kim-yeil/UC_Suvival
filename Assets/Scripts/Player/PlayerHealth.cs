using UnityEngine;
using UnityEngine.Lumin;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;

    private bool isDead;
    private bool isInvincible;

    private Animator playerAnimator;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        isInvincible = false;
        playerAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead == true) return;
        if (isInvincible == true) return;

        currentHealth -= damageAmount;
        Debug.Log("HP : " + currentHealth + " / " + maxHealth);

        isInvincible = true;
        Invoke("DisableInvincible", 1.0f);

        if (currentHealth <= 0)
        {
            Die(); //»ç¸ÁÃ³¸®
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("»ç¸Á!");
        playerAnimator.SetTrigger("Die");
    }

    void DisableInvincible()
    {
        isInvincible = false;
    }
}
