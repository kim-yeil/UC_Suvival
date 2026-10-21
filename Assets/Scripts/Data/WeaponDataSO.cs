using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    public ProjectileMovement projectilePrefab;
    public float projectileSpeed = 5.0f;
    public float attackInterval = 0.7f;
    public int projectileCount = 1;
    public float spreadAngle = 7.5f;
    public int baseDamage = 1;
}
