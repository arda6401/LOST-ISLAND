using Mirror;
using Unity.Cinemachine;
using UnityEngine;

public class Camerafollower : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        CinemachineCamera playerCamera = FindFirstObjectByType<CinemachineCamera>();

        if (playerCamera == null)
        {
            Debug.LogError("Not found cinemachine camera....");
            return;
        }

        playerCamera.Follow = transform;


        Debug.Log("Local Player kamera takibine bağlandı.");
    }
}
