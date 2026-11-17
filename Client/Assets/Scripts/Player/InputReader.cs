using System;

using UnityEngine;


namespace LostIsland.Inputs
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "LostIsland/Input Reader")]
    public class InputReader : ScriptableObject
    {
        private PlayerInput actions;
        public event Action<Vector2> MoveChanged;
        public event Action<Vector2> AimChanged;
        public Vector2 MoveInput { get; private set; }
        public Vector2 AimInput { get; private set; }
        public void Enable()
        {
            if (actions != null) return;

            actions = new PlayerInput();
            actions.GamePlay.Move.performed += OnMove;
            actions.GamePlay.Move.canceled += OnMove;
            actions.GamePlay.Aim.performed += OnAim;
            actions.GamePlay.Aim.canceled += OnAim;

            actions.GamePlay.Enable();
        }

        private void OnDisable() => Disable();






        public void Disable()
        {
            if (actions == null) return;

            actions.GamePlay.Move.performed -= OnMove;
            actions.GamePlay.Move.canceled -= OnMove;
            actions.GamePlay.Aim.performed -= OnAim;
            actions.GamePlay.Aim.canceled -= OnAim;
            actions.GamePlay.Disable();
            actions.Dispose();
            actions = null;

            MoveInput = Vector2.zero;
            AimInput = Vector2.zero;

            AimChanged?.Invoke(AimInput);
            MoveChanged?.Invoke(MoveInput);
        }





        private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
            MoveChanged?.Invoke(MoveInput);
        }
        private void OnAim(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            AimInput = context.ReadValue<Vector2>();
            AimChanged?.Invoke(AimInput);
        }


    }
}