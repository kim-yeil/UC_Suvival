using UnityEngine;

public enum UpgradeNames
{
    MaxHealthIncrease,
    Heal,
    HpRegen,
    MoveSpeed,
    BulletDamage,
    BulletSpread,
    BulletSpeed,
    BullletInterval,
    SuccedingBullet
}

[CreateAssetMenu(fileName = "UpgradeLevelData", menuName = "Game/Upgrade Level Data")]
public class UpgradeLevelDataSO : ScriptableObject
{
    public int[] maxUpgradeLevels
        = new int[]
            {1000, // 최대 체력
            1000,  // 힐
            3,     // 체력 리젠
            6,     // 이동 속도
            1000,  // 총알 데미지
            4,     // 총알 퍼짐 개수
            6,     // 총알 속도
            5,     // 총알 쿨타임
            4,     // 총알 연속 수
            1,     // 후방 공격 활성화
            1000,  // 후방 공격 데미지
            2,     // 후방 공격 퍼짐 개수
            4     // 후방 공격 쿨타임
        };

    public string[] upgradeTexts
        = {"Max Health + 4", // Player Passives
        "Heal 2 HP",
        "HP Regen + 1 HP/5s",
        "Move Speed + 0.5",
        "Bullet Damage + 1", // Bullet Upgrades
        "Bullet Spread + 1",
        "Bullet Speed + 0.5",
        "Bullet Projectile Interval - 0.1",
        "Succeding Bullet + 1",
        "Enable Back Bullet (D1 / 3s)",
        "Back Bullet Damage + 1",
        "Back Bullet Spread + 1",
        "Back Bullet Interval - 0.1"};


}
