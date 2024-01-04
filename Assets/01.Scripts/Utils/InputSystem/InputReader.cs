using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputControl
{
    [CreateAssetMenu(menuName = "SO/New Input System/InputReader")]
    public class InputReader : ScriptableObject, InputControls.IPlayerActions
    {
        public delegate void InputEventListener();
        public delegate void InputEventListener<in T>(T value);
        
        public event InputEventListener OnMouseLeftClickEvent = null;
        public event InputEventListener OnMouseRightClickEvent = null;
        public event InputEventListener<bool> OnLineConnectEvent = null;
        public event InputEventListener OnESCInputEvent = null;

        private InputControls _inputControls;

        [HideInInspector] 
        public Vector3 movementInput;
        public Vector2 mouseScreenPos;

        private void OnEnable()
        {
            if (_inputControls == null)
            {
                _inputControls = new InputControls();
                _inputControls.Player.SetCallbacks(this);
            }
            
            _inputControls.Player.Enable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>(); 
            movementInput = new Vector3(input.x, 0, input.y);
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnMouseLeftClickEvent?.Invoke();
            }
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            mouseScreenPos = context.ReadValue<Vector2>();
        }

        public void OnCharging(InputAction.CallbackContext context)
        {
            if (context.started || context.canceled)
            {
                OnMouseRightClickEvent?.Invoke();
            }
        }

        public void OnLineConnect(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnLineConnectEvent?.Invoke(true);
            }
            else if (context.canceled)
            {
                OnLineConnectEvent?.Invoke(false);
            }
        }

        public void OnESC(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnESCInputEvent?.Invoke();
            }
        }
    }
}