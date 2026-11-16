using System;
using Mirror;
using NUnit.Framework;
using UnityEngine;



namespace LostIsland.Network
{
    public class NetworkBootStap : MonoBehaviour
    {
        [SerializeField] private int physicsHz = 30;
        [SerializeField] private int serverTargetFrameRate = 60;
        [SerializeField] private ushort defaultPort = 7777;



        private void Awake()
        {
            Time.fixedDeltaTime = 1f / physicsHz;
        }

        private void Start()
        {
            bool runAsServer = Application.isBatchMode || HasArg("-server");
            if (!runAsServer) return;
            if (NetworkServer.active) return;
            Debug.Log("[LostIsland] starting Dedicated server...");

            Application.targetFrameRate = serverTargetFrameRate;

            NetworkManager.singleton.StartServer();


            NetworkServer.OnConnectedEvent += OnClientConnected;
            NetworkServer.OnDisconnectedEvent += OnClientDisconnected;
        }
        private void OnDestroy()
        {
            NetworkServer.OnConnectedEvent -= OnClientConnected;
            NetworkServer.OnDisconnectedEvent -= OnClientDisconnected;
        }

        




        private static void OnClientConnected(NetworkConnectionToClient conn)
        {
            Debug.Log($"[Server] Client connected. id={conn.connectionId}");
        }

        private static void OnClientDisconnected(NetworkConnectionToClient conn)
        {
            Debug.Log($"[Server] Client disconnected. id={conn.connectionId}");
        }

        private static bool HasArg(string name)
        {
            foreach (string a in Environment.GetCommandLineArgs())
                if (string.Equals(a, name, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }
}