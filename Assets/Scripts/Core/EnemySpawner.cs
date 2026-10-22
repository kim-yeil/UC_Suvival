using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public enum EnemyIndex
{
    Normal,
    Fast,
    Heavy
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyCatalogSO enemyCatalog;

    [SerializeField] private EnemyChaser[] enemyPrefab;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnInterval = 2.0f;

    [SerializeField] private float minSpawnDistance = 6.0f;
    [SerializeField] private float maxSpawnDistance = 9.0f;

    [SerializeField] private float minVelocity = 1.0f;
    [SerializeField] private float maxVelocity = 3.0f;

    private int spawnCount;
    private float spawnTimer;
    private float spawnedEnemySpeed;
    private int spawnedEnemyExpAmount;

    private WaveInfo currentWaveData;

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
                newEnemy.SetExpAmount(spawnedEnemyExpAmount);
            }
        }
    }

    EnemyChaser GetEnemyPrefab()
    {
        if (currentWaveData == null || enemyCatalog == null) return null;

        int totalWeight = 0; // 적 가중치의 총합
        foreach (EnemyWeight entry in currentWaveData.enemyWeights)
        {
            if (entry.weight > 0) totalWeight += entry.weight;
        }

        int randomInt = Random.Range(0, totalWeight);
        int accumulatedWeight = 0;
        foreach (EnemyWeight entry in currentWaveData.enemyWeights)
        {
            if (entry.weight <= 0) continue;

            accumulatedWeight += entry.weight;
            if (randomInt < accumulatedWeight)
            {
                GameObject obj = enemyCatalog.FindPrefab(entry.enemyId);
                EnemyChaser enemyChaser = obj.GetComponent<EnemyChaser>();
                return enemyChaser;
            }
        }

        return null;
    }

    public void SetWaveData(WaveInfo newWaveData)
    {
        currentWaveData = null;

        int totalWeight = 0;
        foreach(EnemyWeight entry in newWaveData.enemyWeights)
        {
            totalWeight += entry.weight;
        }

        if (totalWeight <= 0) return; // 실수로 해당 웨이브의 적 가중치가 모두 0일 경우 스킵

        currentWaveData = newWaveData;
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
