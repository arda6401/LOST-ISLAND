using UnityEngine;
using Mirror;
using LostIsland.Inputs;
using System;
public class PlayerInputSender : NetworkBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float sendrate = 20f;











    #region  Client


    // Client
    private bool IsListening = false;
    private Vector2 lastMoveInput = Vector2.zero;
    private float NextSendTime = 0f;


    public override void OnStartLocalPlayer()
    {
        inputReader.MoveChanged += MoveChanged;
        inputReader.Enable();
        IsListening = true;
    }


    void Update()
    {

        if (!isLocalPlayer && !IsListening) return;
        if (Time.unscaledTime < NextSendTime) return;

        NextSendTime = Time.unscaledTime + 1f / sendrate;

        CmdSetMove(lastMoveInput);
    }

    public override void OnStopLocalPlayer() => Cleanup();

    private void OnDestroy() => Cleanup();






    private void MoveChanged(Vector2 move)
    {
        lastMoveInput = move;
    }
    private void Cleanup()
    {
        if (!IsListening) return;

        inputReader.MoveChanged -= MoveChanged;
        inputReader.Disable();
        IsListening = false;
        lastMoveInput = Vector2.zero;
    }



    #endregion


    #region  Server
    // Server

    public Vector2 ServerMove { get; private set; }
    public float ServerLastInputTime { get; private set; }




    [Command(channel = Channels.Unreliable)]
    private void CmdSetMove(Vector2 move)
    {
        if (!float.IsFinite(move.x) || !float.IsFinite(move.y))
        {
            Debug.LogWarning($"Invalid move input {move}");
            return;
        }
        if (move.sqrMagnitude > 1f) move.Normalize(); // değer büyükse normalize et




        ServerMove = move;
        ServerLastInputTime = Time.time;
    }


    #endregion

}
