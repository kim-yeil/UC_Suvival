using UnityEngine;

public class CrossAutoAttack : WeaponBase
{
    Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private void Awake()
    {
        InitializeWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttack() == false) return;

        for (int i = 0; i < directions.Length; i++) FireProjectile(directions[i]);

        FinishAttack();
    }
}
