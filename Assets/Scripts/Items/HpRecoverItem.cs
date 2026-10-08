using UnityEngine;

public class HpRecoverItem : MonoBehaviour
{
    private int hpRecoverAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;

        hpRecoverAmount = Random.Range(2, 5);
        playerHealth.GetHpRecoverItem(hpRecoverAmount);
        Destroy(gameObject);
    }
}
