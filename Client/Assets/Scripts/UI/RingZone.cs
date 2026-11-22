using System.Collections.Generic;
using Mirror;
using UnityEngine;


namespace LostIsland.Items
{

    [RequireComponent(typeof(SphereCollider))]
    public class RingZone : MonoBehaviour
    {
        [SerializeField] private GameObject Canvas;

        private readonly HashSet<NetworkIdentity> inside = new HashSet<NetworkIdentity>();



        private void OnTriggerEnter(Collider other)
        {
            if (!NetworkClient.active) return;
            NetworkIdentity identity = other.GetComponentInParent<NetworkIdentity>();
            if (identity == null) return;

            inside.Add(identity);

            if (Canvas.activeSelf) return;

            Canvas.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!NetworkClient.active) return;

            NetworkIdentity identity = other.GetComponentInParent<NetworkIdentity>();
            if (identity == null) return;

            if (inside.Contains(identity))
                inside.Remove(identity);

            if (inside.Count == 0)
            {
                Canvas.SetActive(false);
            }
        }






    }
}