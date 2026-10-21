using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float hpRegenInterval = 5.0f;

    private int increasedMaxHealth;
    private int hpRegenAmount;
    private float nextHPRegenTime;

    private void Reset()
    {
        if (playerHealth == null) playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpRegenAmount <= 0) return;
        if (Time.time < nextHPRegenTime) return;

        playerHealth.Heal(hpRegenAmount);
        nextHPRegenTime = Time.time + hpRegenInterval;
    }

    public void MaxHealthIncrease(int amount)
    {
        increasedMaxHealth += amount;
        playerHealth.MaxHealthIncrease(amount);
    }

    public void HPRegenIncrease(int amount)
    {
        hpRegenAmount += amount;
        nextHPRegenTime = Time.time + hpRegenInterval;
    }
}
