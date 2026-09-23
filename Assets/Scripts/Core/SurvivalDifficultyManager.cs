using System.Runtime.CompilerServices;
using UnityEngine;

public class SurvivalDifficultyManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float initialSpawnInterval = 2.0f;

    [SerializeField] private float difficultyIncreaseInterval = 15.0f;
    [SerializeField] private float spawnintervalDecrease = 0.2f;
    [SerializeField] private float minSpawnInterval = 0.5f;

    private float survivalTime;
    private float difficultyTimer;
    private int difficultyLevel = 1;
    private float currentSpawnInterval;

    private int initialSpawnCount = 1;
    private int currentSpawnCount;
    private int maxSpawnCount = 4;

    private void Awake()
    {
        currentSpawnInterval = initialSpawnInterval;
        currentSpawnCount = initialSpawnCount;
        if (enemySpawner != null)
        {
            enemySpawner.SetSpawnInterval(currentSpawnInterval);
            enemySpawner.SetSpawnCount(currentSpawnCount);
        }
        Debug.Log("시작 난이도 : " + difficultyLevel);
    }

    // Update is called once per frame
    void Update()
    {
        // 난이도 관련 처리.
        CountTime();

    }

    void CountTime()
    {
        survivalTime += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (difficultyTimer < difficultyIncreaseInterval) return;

        // 난이도 상승
        IncreaseDifficulty();

        difficultyTimer = 0.0f;
    }

    void IncreaseDifficulty()
    {
        difficultyLevel++;
        currentSpawnInterval -= spawnintervalDecrease;
        if (currentSpawnInterval < minSpawnInterval) currentSpawnInterval = minSpawnInterval;

        if (enemySpawner != null)
        {
            enemySpawner.SetSpawnInterval(currentSpawnInterval);

            if (difficultyLevel%3 == 0)
            {
                currentSpawnCount++;
                if (currentSpawnCount > maxSpawnCount) currentSpawnCount = maxSpawnCount;
                enemySpawner.SetSpawnCount(currentSpawnCount);
            }
        }

        Debug.Log("레벨 : " + difficultyLevel + " / 마리 : " + currentSpawnCount);
    }
}
