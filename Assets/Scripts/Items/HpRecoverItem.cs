using UnityEngine;

public class HpRecoverItem : MonoBehaviour
{
    private int hpRecoverAmount;

    public void Initialize()
    {
        hpRecoverAmount = Random.Range(2, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;

        playerHealth.Heal(hpRecoverAmount);
        Destroy(gameObject);
    }
}
