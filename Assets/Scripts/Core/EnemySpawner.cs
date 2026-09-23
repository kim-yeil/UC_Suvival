using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyChaser[] enemyPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnInterval = 2.0f;

    [SerializeField] private float minSpawnDistance = 5.0f;
    [SerializeField] private float maxSpawnDistance = 8.0f;

    [SerializeField] private float minVelocity = 1.0f;
    [SerializeField] private float maxVelocity = 3.0f;

    private int spawnCount;

    private float spawnTimer;

    // Update is called once per frame
    void Update()
    {
        CountSpawnTime();
    }

    void CountSpawnTime()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    void SpawnEnemy()
    {
        // 생성 위치 계산
        for (int i = 0; i < spawnCount; i++)
        {
            int enemyNum = Random.Range(0, 3);
            EnemyChaser newEnemy = Instantiate(enemyPrefab[enemyNum],
                GetSpawnPosition(), Quaternion.identity);

            if (newEnemy != null)
            {
                newEnemy.SetTarget(playerTransform);
                newEnemy.SetVelocity(Random.Range(minVelocity, maxVelocity));
            }
        }
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 playerPosition = playerTransform.position;

        return (randomDirection * randomDistance) + playerPosition;
    }

    public void SetSpawnInterval(float newSpawnInterval)
    {
        spawnInterval = newSpawnInterval;
    }

    public void SetSpawnCount(int spawnCount)
    {
        this.spawnCount = spawnCount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, minSpawnDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerTransform.position, maxSpawnDistance);
    }
}
