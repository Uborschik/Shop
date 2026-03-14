using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [Serializable]
    public class TraderCameraConfig
    {
        public Camera TraderCamera;
        public float MouseSensitivity = 0.15f;
    }

    public class TraderLook
    {
        private readonly Transform playerTransform;
        private readonly Transform cameraTransform;
        private float xRotation;

        private readonly TraderCameraConfig cameraConfig;

        public TraderLook(CharacterController characterController, TraderCameraConfig cameraConfig)
        {
            playerTransform = characterController.transform;
            this.cameraConfig = cameraConfig;

            cameraTransform = cameraConfig.TraderCamera.transform;
        }

        public void AddLook(Vector2 delta)
        {
            var mouseX = delta.x * cameraConfig.MouseSensitivity;
            var mouseY = delta.y * cameraConfig.MouseSensitivity;

            playerTransform.Rotate(Vector3.up, mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}