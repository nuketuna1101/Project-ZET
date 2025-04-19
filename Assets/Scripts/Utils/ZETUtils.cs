using UnityEngine;

public static class ZETUtils
{
    public static float GetDistance(Transform _TransformA, Transform _TransformB)
    {
        return Vector3.Distance(_TransformA.position, _TransformB.position);
    }
    public static Vector2 GetDirection(Transform _SrcTransform, Transform _DestTransform)
    {
        return (_DestTransform.position - _SrcTransform.position).normalized;
    }
}