using Mirror;
using UnityEngine;
using LostIsland.Combat;


namespace LostIsland.Player
{

    [RequireComponent(typeof(PlayerInputSender), typeof(CharacterController), typeof(Health))]
    public class PlayerMotor : NetworkBehaviour
    {
        [SerializeField] private MoveParams moveParams;


        private float verticalVelocity;
        private CharacterController controller;
        private PlayerInputSender input;
        private Health health;



        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputSender>();
            health = GetComponent<Health>();
        }


        public override void OnStartClient()
        {
            if (!isServer) controller.enabled = false; //  sunucu değilse kapat 


        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            health.ServerDied += OnDied; 
        }
        public override void OnStopServer()
        {
            base.OnStopServer();
            health.ServerDied -= OnDied;
        }

        [ServerCallback]


        private void FixedUpdate()
        {

            if(health.IsDead) return;

            float dt = Time.fixedDeltaTime;
            Vector2 Move = input.GetMove(moveParams.inputTimeout);


            Vector3 velocity = new Vector3(Move.x, 0f, Move.y) * moveParams.moveSpeed;
            if (controller.isGrounded && verticalVelocity < 0f)
                verticalVelocity = -2f;
            else
                verticalVelocity += moveParams.gravity * dt;




            velocity.y = verticalVelocity;
            controller.Move(velocity * Time.fixedDeltaTime);


            Quaternion target = Quaternion.Euler(0f,input.ServerAimAngle,0f);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,target,moveParams.turnSpeed * dt);
                            
        }

        private void OnDied()
        {
             controller.enabled = false;
             Debug.Log($"[Server] {name} died");
        }
    }
}