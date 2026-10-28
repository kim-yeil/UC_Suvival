using TMPro;
using UnityEngine;

public class DummyTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private TMP_Text hitCountText;
    private int hitCount = 0;

    public void TakeDamage(int damageAmount)
    {
        hitCount++;
        hitCountText.text = "HitCount : " + hitCount.ToString();
    }
}
