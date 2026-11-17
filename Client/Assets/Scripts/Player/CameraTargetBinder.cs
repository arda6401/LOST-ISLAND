using LostIsland.Core;
using Unity.Cinemachine;
using UnityEngine;

public class CameraTargetBinder : MonoBehaviour
{



    private CinemachineCamera cam;

    void Awake()
    {
        cam = GetComponent<CinemachineCamera>();

        if (cam == null)
            Debug.LogError("camera is not found!");
    }
    void Start()
    {
          LocalPlayerEvents.Spawned += SetTarget;
          LocalPlayerEvents.Despawned += ClearTarget;
    }



    private void SetTarget(Transform player)
    {
        cam.Follow = player;

    }

    private void ClearTarget()
    {
                cam.Follow = null;

    }


}
