using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int enemyKillCounts = 0;

    [SerializeField] private TMP_Text enemyKillCountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyKilled()
    {
        enemyKillCounts++;
        enemyKillCountText.text = "Kill Count : " + enemyKillCounts.ToString();
    }
}
