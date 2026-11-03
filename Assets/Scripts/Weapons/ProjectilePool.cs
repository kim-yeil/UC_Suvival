using UnityEngine;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private int initialSize = 30;
    private GameObject projectilePrefab;

    private Queue<ProjectileMovement> availableProjectiles
        = new Queue<ProjectileMovement>();

    public void Configure(GameObject prefab)
    {
        if (prefab == null || prefab.GetComponent<ProjectileMovement>() == null) return;

        projectilePrefab = prefab;

        for (int i = 0; i < initialSize; i++)
        {
            // 총알 생성
            ProjectileMovement projectile = CreateProjectile();
            if (projectile != null) availableProjectiles.Enqueue(projectile);
        }
    }

    ProjectileMovement CreateProjectile()
    {
        GameObject instance = Instantiate(projectilePrefab, transform); // 자식으로 생성

        ProjectileMovement projectile = instance.GetComponent<ProjectileMovement>();
        if (projectile == null)
        {
            Destroy(instance);
            return null;
        }

        projectile.SetPool(this);
        instance.SetActive(false);
        return projectile;
    }

    public ProjectileMovement Get(Vector2 position)
    {
        ProjectileMovement projectile = null;

        if (availableProjectiles.Count > 0) projectile = availableProjectiles.Dequeue();
        else projectile = CreateProjectile();

        projectile.transform.SetParent(null); // 부모 해제
        projectile.transform.position = position;
        projectile.transform.rotation = Quaternion.identity;
        projectile.gameObject.SetActive(true);
        return projectile;
    }

    public void Release(ProjectileMovement projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform); // 부모 재설정
        availableProjectiles.Enqueue(projectile);
    }
}
