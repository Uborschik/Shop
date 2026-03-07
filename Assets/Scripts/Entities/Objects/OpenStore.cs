using DG.Tweening;
using Game.Entities.Tools;
using Game.Services.InputSystem;
using System;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class OpenStore : MonoBehaviour, IInteractable
    {
        //public event Action<bool> SetState;

        [SerializeField] private Open open;
        [SerializeField] private Transform button;
        [SerializeField] private float duration;
        [SerializeField] private float up = 0.1f;
        [SerializeField] private float dawn = 0.05f;

        private bool isPressed;

        public void Interact(IInteractor interactor, InteractionMode mode)
        {
            button.DOKill();
            button.DOLocalMoveY(!isPressed ? dawn : up, duration);
            isPressed = !isPressed;
            open.SendEventMessage();
        }
    }
}