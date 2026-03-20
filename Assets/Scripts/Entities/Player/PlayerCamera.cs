using Game.Core.Controllable;
using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [Serializable]
    public class PlayerCameraConfig
    {
        public Camera Camera;
        public float MouseSensitivity = 0.15f;
        public float MinPitch = -80f;
        public float MaxPitch = 80f;
        public float RotationSmoothTime = 0.05f;
    }

    public class PlayerCamera
    {
        private readonly Camera mainCamera;
        private readonly float mouseSensitivity;
        private readonly float minPitch;
        private readonly float maxPitch;
        private readonly float rotationSmoothTime;

        public Transform Transform { get; private set; }
        public Ray Ray => mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        private Transform target;
        private float currentPitch;
        private float currentYaw;

        private float targetYaw;
        private float targetPitch;

        private float yawVelocity;
        private float pitchVelocity;

        public PlayerCamera(PlayerCameraConfig config)
        {
            mainCamera = config.Camera;
            mouseSensitivity = config.MouseSensitivity;
            minPitch = config.MinPitch;
            maxPitch = config.MaxPitch;
            rotationSmoothTime = config.RotationSmoothTime;

            Transform = mainCamera.transform;
        }

        public void AttachTo(IBody body)
        {
            target = body.Transform;

            if (target != null)
            {
                float startYaw = target.eulerAngles.y;
                targetYaw = startYaw;
                currentYaw = startYaw;
            }
        }

        public void LateTick(Vector2 mouseDelta)
        {
            if (target == null) return;

            targetYaw += mouseDelta.x * mouseSensitivity;
            targetPitch -= mouseDelta.y * mouseSensitivity;
            targetPitch = Mathf.Clamp(targetPitch, minPitch, maxPitch);

            currentYaw = Mathf.SmoothDampAngle(currentYaw, targetYaw, ref yawVelocity, rotationSmoothTime);
            currentPitch = Mathf.SmoothDamp(currentPitch, targetPitch, ref pitchVelocity, rotationSmoothTime);

            var targetPosition = target.position + Vector3.up * 1.7f;
            Transform.SetPositionAndRotation(targetPosition, Quaternion.Euler(currentPitch, currentYaw, 0f));

            target.rotation = Quaternion.Euler(0f, currentYaw, 0f);
        }
    }
}