using UnityEngine;

/// <summary>
/// 투사체의 이동을 담당하는 클래스
/// </summary>
public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2.0f;

    [SerializeField] private Rigidbody2D body;

    private Vector2 moveDirection;
    private float moveSpeed;
    private bool isInitilaized = false;

    [SerializeField] private int damageAmount = 1;
    [SerializeField] private int pierceCount = 1;
    private int remainingPierceCount;

    private ProjectilePool ownerPool;

    private void Reset()
    {
        if (body == null) body = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        remainingPierceCount = pierceCount;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }    

    /// <summary>
    /// prefab 생성 후 초기화하는 함수
    /// </summary>
    /// <param name="direction"> 투사체의 진행 방향 </param>
    /// <param name="speed"> 투사체의 이동 속도 </param>
    public void Initialized(Vector2 direction, float speed)
    {
        moveDirection = direction.normalized;
        moveSpeed = speed;

        isInitilaized = true;
    }

    public void SetBaseDamage(int amount)
    {
        damageAmount = amount;
    } 

    public void DamageIncrease(int amount)
    {
        damageAmount += amount;
    }

    /// <summary>
    /// 투사체의 방향과 속도가 정확히 적용 된 후에 투사체를 이동시킴
    /// </summary>
    private void FixedUpdate()
    {
        if (isInitilaized == false) return;

        body.linearVelocity = moveDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();
        if (target == null) return;
        target.TakeDamage(damageAmount);

        remainingPierceCount--;
        if (remainingPierceCount <= 0) ReturnPool();
    }

    public void SetPool(ProjectilePool pool)
    {
        ownerPool = pool;
    }

    void ReturnPool()
    {
        if (ownerPool != null) ownerPool.Release(this); // 총알 자신을 매개변수로
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        Invoke(nameof(ReturnPool), lifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke(); // Invoke 되기 전에 disable 될 경우 Invoke 취소
        ResetState();
    }

    void ResetState()
    {
        remainingPierceCount = pierceCount;
        moveDirection = Vector2.zero;
        moveSpeed = 0;
        isInitilaized = false;
        body.linearVelocity = Vector2.zero;
    }
}
