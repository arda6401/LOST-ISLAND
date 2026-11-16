using UnityEngine;

namespace LostIsland.Player
{
    [CreateAssetMenu(fileName = "MoveParams_Default", menuName = "LostIsland/Move Params")]
    public class MoveParams : ScriptableObject
    {
        public float moveSpeed = 5f;
        public float gravity = -20f;
        public float inputTimeout = 0.25f;
    }
}