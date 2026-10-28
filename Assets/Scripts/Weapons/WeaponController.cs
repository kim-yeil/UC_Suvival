using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponBase[] weapons;
    [SerializeField] private PlayerHealth playerHealth; 

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.isDead == true) return;

        foreach(WeaponBase weapon in weapons)
        {
            weapon.TryAttack();
        }
    }
}
