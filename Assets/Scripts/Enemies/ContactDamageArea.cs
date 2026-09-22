using UnityEngine;

public class ContactDamageArea : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    /// <summary>
    /// 오브젝트가 Trigger 충돌 상태를 지속할 때 계속 호출
    /// </summary>
    /// <param name="collision"> 충돌한 대상의 Collider2D 정보 </param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;

        playerHealth.TakeDamage(damageAmount);
    }
}
