using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 클래스의 직렬화 -> Inspector 창에 표시됨
/// </summary>
[Serializable]
public class EnemyInfo
{
    public string id;
    public GameObject enemyPrefab;
}

[CreateAssetMenu(fileName = "EnemyCatalog", menuName = "Game/Enemy Catalog")]
public class EnemyCatalogSO : ScriptableObject
{
    public List<EnemyInfo> enemies = new List<EnemyInfo>();

    public GameObject FindPrefab(string enemyId)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].id == enemyId) return enemies[i].enemyPrefab;
        }

        return null;
    }
}
