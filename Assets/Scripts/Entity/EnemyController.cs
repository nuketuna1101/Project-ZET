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
    }

    public override void Death()
    {
        base.Death();
        _EnemyManager.RemoveEnemyOnDeath(this);
    }
}
