using LostIsland.Core;
using Mirror;


namespace LostIsland.Player
{
    public class LocalPlayerNotifier : NetworkBehaviour
    {
        public override void OnStartLocalPlayer() => LocalPlayerEvents.RaiseSpawned(transform);
        public override void OnStopLocalPlayer() => LocalPlayerEvents.RaiseDespawned();
    }
}
