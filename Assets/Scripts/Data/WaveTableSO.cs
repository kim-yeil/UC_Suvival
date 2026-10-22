using UnityEngine;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

[Serializable]
public class EnemyWeight
{
    public string enemyId;
    public int weight = 1; // default = 1;
}

[Serializable]
public class WaveInfo
{
    public string waveName;
    public List<EnemyWeight> enemyWeights = new List<EnemyWeight>();
}

[CreateAssetMenu(fileName = "WaveTable", menuName = "Game/Wave Table")]
public class WaveTableSO : ScriptableObject
{
    public List<WaveInfo> waves = new List<WaveInfo>();

    public WaveInfo GetWave(int difficultyLevel)
    {
        int index = Mathf.Clamp(difficultyLevel - 1, 0, waves.Count - 1);
        return waves[index];
    }
}
