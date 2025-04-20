using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager _EnemyManager;
    private Transform target;

    [SerializeField] private float followRange = 15f;

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

    public override void Death()
    {
        //base.Death();
        _AnimationHandler.GetKilled();

        _Rigidbody.linearVelocity = Vector3.zero;
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            // AnimationHandler 타입의 컴포넌트는 비활성화하지 않음
            if (component.GetType() != typeof(AnimationHandler))
            {
                component.enabled = false;
            }
        }
        Destroy(gameObject, 2f);
        _EnemyManager.RemoveEnemyOnDeath(this);
    }
}
