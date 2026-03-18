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
    }

    public class PlayerCamera
    {
        private readonly Camera currentCamera;
        private readonly float mouseSensitivity;
        private readonly float minPitch;
        private readonly float maxPitch;

        public Transform Transform { get; private set; }

        private Transform target;
        private float currentPitch;
        private float currentYaw;

        public PlayerCamera(PlayerCameraConfig config)
        {
            currentCamera = config.Camera;
            mouseSensitivity = config.MouseSensitivity;
            minPitch = config.MinPitch;
            maxPitch = config.MaxPitch;

            Transform = currentCamera.transform;
        }

        public void AttachTo(IBody body)
        {
            target = body.Transform;
            Transform.SetParent(null);

            if (target != null) currentYaw = target.eulerAngles.y;
        }

        public void LateTick(Vector2 mouseDelta)
        {
            if (target == null) return;

            currentYaw += mouseDelta.x * mouseSensitivity;
            currentPitch -= mouseDelta.y * mouseSensitivity;
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

            var targetPosition = target.position + Vector3.up * 1.7f;
            Transform.SetPositionAndRotation(targetPosition, Quaternion.Euler(currentPitch, currentYaw, 0f));

            target.rotation = Quaternion.Euler(0f, currentYaw, 0f);
        }
    }
}