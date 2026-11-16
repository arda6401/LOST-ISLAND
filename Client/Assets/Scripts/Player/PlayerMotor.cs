using Mirror;
using UnityEngine;


namespace LostIsland.Player
{

    [RequireComponent(typeof(PlayerInputSender), typeof(CharacterController))]
    public class PlayerMotor : NetworkBehaviour
    {
        [SerializeField] private MoveParams moveParams;


        private float verticalVelocity;
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

            float dt = Time.fixedDeltaTime;
            Vector2 Move = input.GetMove(moveParams.inputTimeout);


            Vector3 velocity = new Vector3(Move.x, 0f, Move.y) * moveParams.moveSpeed;
            if (controller.isGrounded && verticalVelocity < 0f)
                verticalVelocity = -2f;
            else
                verticalVelocity += moveParams.gravity * dt;




            velocity.y = verticalVelocity;
            controller.Move(velocity * Time.fixedDeltaTime);
        }
    }
}