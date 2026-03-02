using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Player
{
    [Serializable]
    public class CameraConfig
    {
        public float MouseSensitivity = 0.15f;
    }

    public class PawnLook
    {
        private readonly Transform playerTransform;
        private readonly Transform cameraTransform;
        private float xRotation;
        private float yRotation;

        private readonly CameraConfig config;

        public PawnLook(Transform playerTransform, Transform cameraTransform, CameraConfig config)
        {
            this.playerTransform = playerTransform;
            this.cameraTransform = cameraTransform;

            this.config = config;
        }

        public void AddLook(Vector2 delta)
        {
            var mouseX = delta.x * config.MouseSensitivity;
            var mouseY = delta.y * config.MouseSensitivity;

            playerTransform.Rotate(Vector3.up, mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}