using UnityEngine;

public static class Extensions
{
    public static void SetEnabled(this Rigidbody rigidbody, bool enable)
    {
        if (rigidbody.isKinematic == !enable) return;

        rigidbody.linearVelocity = default;
        rigidbody.angularVelocity = default;
        rigidbody.useGravity = enable;
        rigidbody.isKinematic = !enable;
    }
}
