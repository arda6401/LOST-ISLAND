using System;

using UnityEngine;


namespace LostIsland.Inputs
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "LostIsland/Input Reader")]
    public class InputReader : ScriptableObject
    {
        private PlayerInput actions;
        public event Action<Vector2> MoveChanged;
        public Vector2 MoveInput { get; private set; }
        public void Enable()
        {
            if (actions != null) return;

            actions = new PlayerInput();
            actions.GamePlay.Move.performed += OnMove;
            actions.GamePlay.Move.canceled += OnMove;
            actions.GamePlay.Enable();
        }

        private void OnDisable() => Disable();






        public void Disable()
        {
            if (actions == null) return;

            actions.GamePlay.Move.performed -= OnMove;
            actions.GamePlay.Move.canceled -= OnMove;
            actions.GamePlay.Disable();
            actions.Dispose();
            actions = null;

            MoveInput = Vector2.zero;
            MoveChanged?.Invoke(MoveInput);
        }

       



        private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
            MoveChanged?.Invoke(MoveInput);
        }


    }
}