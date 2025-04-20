using UnityEngine;
using System;

public class EnemyController : BaseController, IPoolable
{
    private EnemyManager _EnemyManager;
    private Transform target;
    private Action<GameObject> _returnAction;

    [SerializeField] private float followRange = 15f;

    public void Initialize(Action<GameObject> returnAction)
    {
        _returnAction = returnAction;
    }

    public void OnSpawn()
    {
        // 오브젝트가 풀에서 스폰될 때 초기화
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = true;
        }
        if (GetComponent<AnimationHandler>() != null)
        {
            GetComponent<AnimationHandler>().enabled = true;
        }
        _Rigidbody.linearVelocity = Vector2.zero;
        isDead = false;
    }

    public void OnDespawn()
    {
        // 오브젝트가 풀에 반환될 때 처리
        //if (!moveDirection.Equals(Vector2.zero)) moveDirection = Vector2.zero;
        //isAttacking = false;
        //lookDirection = Vector2.zero;
        //target = null;
        PoolManager.Instance.ReturnObject(0, this.gameObject);
    }

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this._EnemyManager = enemyManager;
        this.target = target;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (target == null)
        {
            if (!moveDirection.Equals(Vector2.zero)) moveDirection = Vector2.zero;
            return;
        }

        float distance = ZETUtils.GetDistance(transform, target);
        Vector2 direction = ZETUtils.GetDirection(transform, target);

        isAttacking = false;
        if (distance <= followRange)
        {
            lookDirection = direction;

            if (distance <= _WeaponHandler.AttackRange)
            {
                int layerMaskTarget = _WeaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _WeaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }

                moveDirection = Vector2.zero;
                return;
            }

            moveDirection = direction;
        }
    }

    private bool isDead = false;

    public override void Death()
    {
        if (isDead) return;
        isDead = true;

        _AnimationHandler.GetKilled();

        _Rigidbody.linearVelocity = Vector2.zero;
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            if (component.GetType() != typeof(AnimationHandler))
            {
                component.enabled = false;
            }
        }

        // Destroy 대신 ReturnObject 호출
        if (_returnAction != null)
        {
            OnDespawn();
            _EnemyManager.RemoveEnemyOnDeath(this);
        }
        else
        {
            Debug.LogError("Return Action is null. Object will be destroyed.");
            Destroy(gameObject);
            _EnemyManager.RemoveEnemyOnDeath(this);
        }
    }
}