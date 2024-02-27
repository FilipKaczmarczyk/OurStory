using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    public class InputReader : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        private PlayerInputActions _playerInputActions;

        private void OnEnable()
        {
            if (_playerInputActions == null)
            {
                _playerInputActions = new PlayerInputActions();

                _playerInputActions.Player.SetCallbacks(this);

                // Activate correct input map
                _playerInputActions.Player.Enable();
            }
        }
        
        // CAMERA MOVEMENT
        public static event Action<Vector2> CameraMoveEvent;
        public static event Action<float> CameraZoomEvent;
        public static event Action<InputAction.CallbackContext> CameraRotateButtonEvent;
        
        // SELECTION
        public static event Action<InputAction.CallbackContext> UnitSelectionButtonEvent;
        public static event Action<InputAction.CallbackContext> MultiSelectionButtonEvent;
        
        // UNIT ACTION
        public static event Action<InputAction.CallbackContext> ActionButtonEvent;

        public void OnCameraMove(InputAction.CallbackContext context)
        {
            CameraMoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnCameraZoom(InputAction.CallbackContext context)
        {
            CameraZoomEvent?.Invoke(context.ReadValue<float>());
        }

        public void OnCameraRotateButton(InputAction.CallbackContext context)
        {
            CameraRotateButtonEvent?.Invoke(context);
        }

        public void OnUnitSelectionButton(InputAction.CallbackContext context)
        {
            UnitSelectionButtonEvent?.Invoke(context);
        }

        public void OnMultiSelectionButton(InputAction.CallbackContext context)
        {
            MultiSelectionButtonEvent?.Invoke(context);
        }

        public void OnActionButton(InputAction.CallbackContext context)
        {
            ActionButtonEvent?.Invoke(context);
        }
    }
}