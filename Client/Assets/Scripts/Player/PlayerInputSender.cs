using UnityEngine;
using Mirror;
using LostIsland.Inputs;
using System;
using Unity.Mathematics;
using LostIsland.Core;
public class PlayerInputSender : NetworkBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float sendrate = 20f;
    private Camera cam;











    #region  Client


    // Client
    private bool IsListening = false;
    private Vector2 lastMoveInput = Vector2.zero;
    private Vector2 CurrentAimInput = Vector2.zero;
    private float lastAimAngle;

    private float NextSendTime = 0f;



    public override void OnStartLocalPlayer()
    {
        inputReader.MoveChanged += MoveChanged;
        inputReader.AimChanged += AimChanged;
        inputReader.Enable();
        IsListening = true;
        cam = Camera.main;
    }


    void Update()
    {

        if (!isLocalPlayer && !IsListening) return;
        if (Time.unscaledTime < NextSendTime) return;

        NextSendTime = Time.unscaledTime + 1f / sendrate;

        CmdSetMove(ToWorldMove(lastMoveInput), AimAngleCode.Encode(GetWorldAimAngle()));
    }

    public override void OnStopLocalPlayer() => Cleanup();

    private void OnDestroy() => Cleanup();



    private float GetWorldAimAngle()
    {
        if (AimMath.TryGetWorldAngle(CurrentAimInput, cam, out float angle))
            lastAimAngle = angle;
        return lastAimAngle;
    }


    private Vector2 ToWorldMove(Vector2 screenMove)
    {
        if (cam == null) return screenMove;

        Quaternion yawonly = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);
        Vector3 world = yawonly * new Vector3(screenMove.x, 0f, screenMove.y);


        return new Vector2(world.x, world.z);
    }


    private void MoveChanged(Vector2 move)
    {
        lastMoveInput = move;
    }
    private void AimChanged(Vector2 aim)
    {
        CurrentAimInput = aim;
    }
    private void Cleanup()
    {
        if (!IsListening) return;

        inputReader.MoveChanged -= MoveChanged;
        inputReader.AimChanged -= AimChanged;
        inputReader.Disable();
        IsListening = false;
        lastMoveInput = Vector2.zero;
    }



    #endregion


    #region  Server
    // Server

    public Vector2 ServerMove { get; private set; }
    public float ServerAimAngle { get; private set; }
    public float ServerLastInputTime { get; private set; }




    [Command(channel = Channels.Unreliable)]
    private void CmdSetMove(Vector2 move, ushort aimangle)
    {
        if (!float.IsFinite(move.x) || !float.IsFinite(move.y))
        {
            Debug.LogWarning($"Invalid move input {move}");
            return;
        }
        if (move.sqrMagnitude > 1f) move.Normalize(); // değer büyükse normalize et



        ServerAimAngle = AimAngleCode.Decode(aimangle);
        ServerMove = move;
        ServerLastInputTime = Time.time;
    }

    public Vector2 GetMove(float maxAge)
    {
        bool fresh = Time.time - ServerLastInputTime <= maxAge;


        return fresh ? ServerMove : Vector2.zero;

    }


    #endregion

}
