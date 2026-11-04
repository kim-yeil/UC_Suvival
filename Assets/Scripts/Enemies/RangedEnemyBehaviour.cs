using UnityEngine;

public class RangedEnemyBehaviour : MonoBehaviour
{
    private EnemyChaser enemyChaser;
    private float attackRange = 5.0f;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    private void Awake()
    {
        enemyChaser = GetComponent<EnemyChaser>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
