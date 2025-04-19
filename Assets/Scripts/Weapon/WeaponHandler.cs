using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Weapon Basic Properties")]
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }


    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }


    public LayerMask target;
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    public BaseController _Controller { get; private set; }
    private Animator _Animator;
    private SpriteRenderer _WeaponRenderer;

    protected virtual void Awake()
    {
        _Controller = GetComponentInParent<BaseController>();
        _Animator = GetComponentInChildren<Animator>();
        _WeaponRenderer = GetComponentInChildren<SpriteRenderer>();

        _Animator.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        _Animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isLeft)
    {
        _WeaponRenderer.flipY = isLeft;
    }


}
