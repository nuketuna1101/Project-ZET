using System.Collections;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [Header("Boolean Variables")]
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsHit = Animator.StringToHash("IsHit");
    private static readonly int IsKilled = Animator.StringToHash("IsKilled");

    protected Animator _Animator;

    protected virtual void Awake()
    {
        _Animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 moveDirection)
    {
        _Animator.SetBool(IsMoving, moveDirection.magnitude > 0.5f);
    }

    public void GetHit()
    {
        _Animator.SetBool(IsHit, true);
    }

    public void GetKilled()
    {
        _Animator.SetBool(IsKilled, true);
    }

    public void GetInvincibilityEnd()
    {
        _Animator.SetBool(IsHit, false);
    }
}
