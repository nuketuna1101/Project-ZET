using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private Camera _Camera;

    protected override void Start()
    {
        base.Start();
        _Camera = Camera.main;
    }

    protected override void HandleAction()
    {
    }

    void OnMove(InputValue inputValue)
    {
        moveDirection = inputValue.Get<Vector2>().normalized;
    }

    void OnLook(InputValue inputValue)
    {
        Vector2 inputWorldPos = _Camera.ScreenToWorldPoint(inputValue.Get<Vector2>());
        lookDirection = inputWorldPos - (Vector2)transform.position;
        lookDirection = (lookDirection.magnitude > 1.0f ? lookDirection.normalized : Vector2.zero);
    }

    void OnFire(InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        isAttacking = inputValue.isPressed;
    }

    public override void Death()
    {
        //base.Death();

        _Rigidbody.linearVelocity = Vector3.zero;
        _AnimationHandler.GetKilled();
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }
    }
}