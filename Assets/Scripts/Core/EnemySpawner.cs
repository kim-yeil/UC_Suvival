using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyIndex
{
    Normal,
    Fast,
    Heavy
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyChaser[] enemyPrefab;
    [SerializeField] private int[] enemyChance = { 65, 25, 10 };
    [SerializeField] private float[] enemySpeed = { 1.5f, 2.5f, 0.7f };

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnInterval = 2.0f;

    [SerializeField] private float minSpawnDistance = 5.0f;
    [SerializeField] private float maxSpawnDistance = 8.0f;

    [SerializeField] private float minVelocity = 1.0f;
    [SerializeField] private float maxVelocity = 3.0f;

    private int spawnCount;
    private float spawnTimer;
    private float spawnedEnemySpeed;

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
            EnemyChaser newEnemy = Instantiate(GetEnemyPrefab(),
                GetSpawnPosition(), Quaternion.identity);

            if (newEnemy != null)
            {
                newEnemy.SetTarget(playerTransform);
                newEnemy.SetVelocity(spawnedEnemySpeed);
            }
        }
    }

    EnemyChaser GetEnemyPrefab()
    {
        int randomValue = Random.Range(0, 100);

        //randomValue가 0 ~ 9 : Heavy / 10 ~ 34 : Fast / 35 ~ 99 : Normal
        if (randomValue < enemyChance[(int)EnemyIndex.Heavy])
        {
            spawnedEnemySpeed = enemySpeed[(int)EnemyIndex.Heavy];
            return enemyPrefab[(int)EnemyIndex.Heavy];
        }
        
        if (randomValue < enemyChance[(int)EnemyIndex.Heavy] + enemyChance[(int)EnemyIndex.Fast])
        {
            spawnedEnemySpeed = enemySpeed[(int)EnemyIndex.Fast];
            return enemyPrefab[(int)EnemyIndex.Fast];
        }

        spawnedEnemySpeed = enemySpeed[(int)EnemyIndex.Normal];
        return enemyPrefab[(int)EnemyIndex.Normal];
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
