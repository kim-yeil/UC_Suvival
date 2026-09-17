using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyChaser[] enemyPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnInterval = 2.0f;

    private float spawnTimer;
    private int spawnPointNumber;
    private int enemyNumber;

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
        EnemyChaser newEnemy = Instantiate(enemyPrefab[enemyNumber], spawnPoints[spawnPointNumber].position, Quaternion.identity);

        if (newEnemy != null)
        {
            newEnemy.SetTarget(playerTransform);
        }
        
        spawnPointNumber = (spawnPointNumber + 1) % spawnPoints.Length;
        enemyNumber = (enemyNumber + 1) % enemyPrefab.Length;
    }
}
