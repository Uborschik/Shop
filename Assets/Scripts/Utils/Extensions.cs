using UnityEngine;

public static class Extensions
{
    public static void SetEnabled(this Rigidbody rigidbody, bool enable)
    {
        if (rigidbody.isKinematic == !enable) return;

        rigidbody.useGravity = enable;
        rigidbody.isKinematic = !enable;
    }
}
