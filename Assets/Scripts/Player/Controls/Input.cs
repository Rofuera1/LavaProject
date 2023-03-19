using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Input : MonoBehaviour
    {
        [SerializeField]
        private float _sensivity;
        [SerializeField]
        private FloatingJoystick _joystick;

        public UnityEngine.Events.UnityEvent OnDrag { get; set; } = new UnityEngine.Events.UnityEvent();
        public bool Dragging { get; private set; }
        public Vector2 DragMagnitude => dragMagnitude();

        private void Awake()
        {
            _joystick.OnDragged.AddListener(onDragStart);
            _joystick.OnStopped.AddListener(onDragEnd);
        }

        private void onDragStart()
        {
            if (Dragging) return;

            Dragging = true;
            OnDrag?.Invoke();
        }

        private void onDragEnd()
        {
            Dragging = false;
        }

        private Vector2 dragMagnitude()
        {
            return _joystick.Direction * _sensivity;
        }
    }
}