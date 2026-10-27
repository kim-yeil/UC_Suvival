using System;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

/// <summary>
/// 클래스의 직렬화 -> Inspector 창에 표시됨
/// </summary>
[Serializable]
public class EnemyInfo
{
    public string id;
    public GameObject enemyPrefab;
    public float enemySpeed;
    public int enemyEXP;
}

[CreateAssetMenu(fileName = "EnemyCatalog", menuName = "Game/Enemy Catalog")]
public class EnemyCatalogSO : ScriptableObject
{
    public List<EnemyInfo> enemies = new List<EnemyInfo>();

    private Dictionary<string, EnemyInfo> enemyLookup;

    void BuildEnemyLookup()
    {
        enemyLookup = new Dictionary<string, EnemyInfo>();

        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyInfo enemy = enemies[i];

            // null 체크
            if (enemy == null || string.IsNullOrEmpty(enemy.id) == true) continue;

            // 중복 ID 체크
            if (enemyLookup.ContainsKey(enemy.id) == true) continue;

            // Dictionary에 추가
            enemyLookup.Add(enemy.id, enemy);
        }
    }
    public EnemyInfo FindEnemy(string enemyId)
    {
        if (enemyLookup == null)
            BuildEnemyLookup();

        if (enemyLookup.TryGetValue(enemyId, out EnemyInfo enemy))
            return enemy;

        return null;
    }

    public GameObject FindPrefab(string enemyId)
    {
        return FindEnemy(enemyId)?.enemyPrefab;
    }

    public float FindEnemySpeed(string enemyId)
    {
        return FindEnemy(enemyId)?.enemySpeed?? 0;
    }

    public int FindEnemyEXP(string enemyId)
    {
        return FindEnemy(enemyId)?.enemyEXP?? 0;
    }
}
