using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _Rigidbody;
    protected AnimationHandler _AnimationHandler;
    protected StatHandler _StatHandler;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;


    [Header("Movement")]
    protected Vector2 moveDirection = Vector2.zero;
    public Vector2 MoveDirection { get { return moveDirection; } }

    [Header("Look")]
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    [Header("Knockback")]
    private Vector2 knockbackDirection = Vector2.zero;
    private float knockbackDuration = 0.0f;


    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler _WeaponHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;


    protected virtual void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _AnimationHandler = GetComponent<AnimationHandler>();
        _StatHandler = GetComponent<StatHandler>();

        if (WeaponPrefab != null)
            _WeaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            _WeaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();
    }
    protected virtual void FixedUpdate()
    {
        UpdateMovment(moveDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void UpdateMovment(Vector2 direction)
    {
        // 이동 업데이트 : speed 기반
        direction = direction * _StatHandler.Speed;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockbackDirection;
        }
        // 속도 적용 및 애니메이션
        _Rigidbody.linearVelocity = direction;
        _AnimationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        // 스프라이트 회전 설정 기본: 오른쪽 바라보기
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        _WeaponHandler?.Rotate(isLeft);

    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockbackDirection = -(other.position - transform.position).normalized * power;
    }


    private void HandleAttackDelay()
    {
        if (_WeaponHandler == null)
            return;

        if (timeSinceLastAttack <= _WeaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > _WeaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            _WeaponHandler?.Attack();
    }

    public virtual void Death()
    {
        _Rigidbody.linearVelocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
