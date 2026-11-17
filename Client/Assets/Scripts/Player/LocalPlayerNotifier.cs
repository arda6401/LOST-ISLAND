using LostIsland.Core;
using Mirror;
using UnityEngine;

public class LocalPlayerNotifier : NetworkBehaviour
{
    public override void OnStartLocalPlayer()=> LocalPlayerEvents.RaiseSpawned(transform);
    public override void OnStopLocalPlayer() => LocalPlayerEvents.RaiseDespawned();

}
