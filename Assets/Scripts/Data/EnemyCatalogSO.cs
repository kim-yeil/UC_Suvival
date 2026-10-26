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
    public float enemySpeed;
    public int enemyEXP;
}

[CreateAssetMenu(fileName = "EnemyCatalog", menuName = "Game/Enemy Catalog")]
public class EnemyCatalogSO : ScriptableObject
{
    public List<EnemyInfo> enemies = new List<EnemyInfo>();

    private Dictionary<string, GameObject> prefabLookup;
    private Dictionary<string, float> enemySpeeds;
    private Dictionary<string, int> enemyEXPs;

    void BuildPrefabLookup()
    {
        prefabLookup = new Dictionary<string, GameObject>();
        enemySpeeds = new Dictionary<string, float>();
        enemyEXPs = new Dictionary<string, int>();

        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyInfo enemy = enemies[i];

            // null 체크
            if (enemy == null || string.IsNullOrEmpty(enemy.id) == true
                || enemy.enemyPrefab == null) continue;

            // 중복 ID 체크
            if (prefabLookup.ContainsKey(enemy.id) == true) continue;

            // Dictionary에 추가
            prefabLookup.Add(enemy.id, enemy.enemyPrefab);
            enemySpeeds.Add(enemy.id, enemy.enemySpeed);
            enemyEXPs.Add(enemy.id, enemy.enemyEXP);
        }
    }

    public GameObject FindPrefab(string enemyId)
    {
        if (prefabLookup == null) BuildPrefabLookup();

        // Dictionary 문법을 이용한 두 가지 방법.

        // 첫번째 방법
        GameObject foundPrefab;

        // out : 매개변수의 값이 아닌 주소 값을 가져옴
        if (prefabLookup.TryGetValue(enemyId, out foundPrefab) == true) return foundPrefab;

        // 두번째 방법
        //if (prefabLookup.ContainsKey(enemyId) == true) return prefabLookup[enemyId];

        return null;
    }

    public float FindEnemySpeed(string enemyId)
    {
        if (enemySpeeds.ContainsKey(enemyId) == true) return enemySpeeds[enemyId];

        return 0;
    }

    public int FindEnemyEXP(string enemyId)
    {
        if (enemyEXPs.ContainsKey(enemyId) == true) return enemyEXPs[enemyId];

        return 0;
    }
}
