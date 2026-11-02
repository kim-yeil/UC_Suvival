using UnityEngine;
using System.Collections; // Coroutine을 사용하기 위한 네임스페이스

public class MiniBossBehaviour : MonoBehaviour
{
    enum MiniBossState
    {
        Chase,
        Prepare,
        Dash,
        Heal,
        Die
    }

    [SerializeField] private float attackRange = 3.0f;
    [SerializeField] private float prepareTime = 0.7f;
    [SerializeField] private float dashSpeed = 9.0f;
    [SerializeField] private float dashTime = 0.35f;
    [SerializeField] private float healTime = 1;

    [SerializeField] private Rigidbody2D body;
    [SerializeField] private EnemyChaser enemyChaser;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private Animator animator;

    private Transform playerTransform;
    private MiniBossState currentState = MiniBossState.Chase;
    private Vector2 dashDirection;
    private Coroutine attackRoutine; // 코루틴 실행 중 죽었을 때를 대비

    public void SetTarget(Transform target)
    {
        playerTransform = target;
    }

    // Update is called once per frame
    void Update()
    {
        DeathCheck();

        if (playerTransform == null || currentState != MiniBossState.Chase) return;

        float distance = Vector2.Distance(body.position, playerTransform.position);
        if (distance > attackRange) return;

        currentState = MiniBossState.Die;
        enemyChaser.enabled = false;
        body.linearVelocity = Vector2.zero;

        // Coroutine 호출 - 공격 시퀀스
        attackRoutine = StartCoroutine(AttackSequence());
    }

    void DeathCheck()
    {
        if (enemyHealth.isDead == true)
        {
            currentState = MiniBossState.Prepare;
            attackRoutine = null;
            body.linearVelocity = Vector2.zero;
        }
    }

    IEnumerator AttackSequence() // private 생략됨
    {
        yield return new WaitForSeconds(prepareTime);

        if (playerTransform != null)
        {
            dashDirection = ((Vector2)playerTransform.position - body.position).normalized;
            currentState = MiniBossState.Dash;

            yield return new WaitForSeconds(dashTime);

            body.linearVelocity = Vector2.zero;
            currentState = MiniBossState.Heal;

            yield return new WaitForSeconds(healTime);

            // 공격 시퀀스 종료
            FinishAttack();
        }
        else
        {
            FinishAttack();
            yield break; // Player == null
        }
    }

    void FinishAttack()
    {
        currentState = MiniBossState.Chase;
        enemyChaser.enabled = true;
        attackRoutine = null; // 코루틴 초기화
    }

    private void FixedUpdate()
    {
        if (currentState == MiniBossState.Dash)
        {
            body.linearVelocity = dashDirection * dashSpeed;
        }
        
    }

    private void OnDisable()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

        if (body != null) body.linearVelocity = Vector2.zero; // 비활성화 시 속도 초기화
    }
}
