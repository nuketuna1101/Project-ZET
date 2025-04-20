using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController _BaseController;
    private StatHandler _StatHandler;
    private AnimationHandler _AnimationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => _StatHandler.Health;

    private void Awake()
    {
        _StatHandler = GetComponent<StatHandler>();
        _AnimationHandler = GetComponent<AnimationHandler>();
        _BaseController = GetComponent<BaseController>();
    }

    private void Start()
    {
        CurrentHealth = _StatHandler.Health;
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                _AnimationHandler.GetInvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change < 0)
        {
            _AnimationHandler.GetHit();

        }

        if (CurrentHealth <= 0f)
        {
            _BaseController.Death();
        }

        return true;
    }
}
