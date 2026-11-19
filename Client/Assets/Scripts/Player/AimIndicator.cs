using System;
using LostIsland.Core;
using LostIsland.Inputs;
using Mirror;
using UnityEngine;



namespace LostIsland.Player
{

    [RequireComponent(typeof(PlayerEntity))]
    public class AimIndicator : NetworkBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private float length = 8f;
        [SerializeField] private float height = 0.1f;
        [SerializeField] private LineRenderer line;
        private Camera cam;
        private float aimAngle;
        private bool isListening;
        private PlayerEntity player;
        private bool Dead = false;


        private void Awake()
        {
            line.positionCount = 2;
            line.useWorldSpace = true;
            line.alignment = LineAlignment.TransformZ;
            line.enabled = false;
            player = GetComponent<PlayerEntity>();
        }



        public override void OnStartLocalPlayer()
        {
            cam = Camera.main;
            inputReader.AimChanged += OnAimChanged;

            isListening = true;
            player.health.Died += OnplayerDead;

        }

        private void OnplayerDead()
        {
            Dead =true;
            line.enabled=false;


        }

        private void LateUpdate()
        {
            if (!line.enabled) return;


            Vector3 start = transform.position + Vector3.up * height;

            Vector3 dir = Quaternion.Euler(0f, aimAngle, 0f) * Vector3.forward;

            line.SetPosition(0, start);
            line.SetPosition(1, start + dir * length);
        }

        public override void OnStopLocalPlayer() => Cleanup();

        private void OnDestroy() => Cleanup();



        private void OnAimChanged(Vector2 aim)
        { 

            if(Dead) return;

            if (AimMath.TryGetWorldAngle(aim, cam, out float angle))
            {
                aimAngle = angle;
                line.enabled = true;
            }
            else
            {
                line.enabled = false;
            }
        }


        private void Cleanup()
        {
            if (!isListening) return;
            inputReader.AimChanged -= OnAimChanged;
            player.health.Died -= OnplayerDead;
            line.enabled = false;
            isListening = false;
        }
    }
}