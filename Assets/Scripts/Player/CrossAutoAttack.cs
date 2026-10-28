using UnityEngine;

public class CrossAutoAttack : WeaponBase
{
    Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private void Awake()
    {
        InitializeWeapon();
    }

    protected override bool Attack()
    {
        bool createAny = false;

        for (int i = 0; i < directions.Length; i++) createAny = FireProjectile(directions[i]);
        return createAny;
    }
}
