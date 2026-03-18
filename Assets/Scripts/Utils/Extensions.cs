using UnityEngine;

public static class Extensions
{
    public static void SetEnabled(this Rigidbody rigidbody, bool enable)
    {
        if (rigidbody.isKinematic == !enable) return;

        rigidbody.useGravity = enable;
        rigidbody.isKinematic = !enable;
    }

    public static bool TryGetComponentInParent<T>(this Collider collider, out T item) where T : class
    {
        item = collider.GetComponentInParent<T>();
        return item != null;
    }

    public static bool TryGetComponentInChildren<T>(this Transform transform, out T item) where T : class
    {
        item = transform.GetComponentInChildren<T>();
        return item != null;
    }
}
