using Mirror;
using UnityEngine;


namespace LostIsland.Player
{

    [RequireComponent(typeof(PlayerInputSender), typeof(CharacterController))]
    public class PlayerMotor : NetworkBehaviour
    {
        [SerializeField] private MoveParams moveParams;



        private CharacterController controller;
        private PlayerInputSender input;



        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputSender>();
        }


        public override void OnStartClient()
        {
            if (!isServer) controller.enabled = false; //  sunucu değilse kapat 
        }


        [ServerCallback]


        private void FixedUpdate()
        {
            Vector2 Move = input.ServerMove;


            Vector3 velocity = new Vector3(Move.x, 0f, Move.y) * moveParams.moveSpeed;
            controller.Move(velocity * Time.fixedDeltaTime);
        }
    }
}