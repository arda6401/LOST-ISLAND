using UnityEngine;



namespace LostIsland.Core
{


    public static class AimMath
    {
        private const float AimDeadzone = 0.2f;
        public static bool TryGetWorldAngle(Vector2 aim, Camera cam, out float angle)
        {

            if (aim.sqrMagnitude < AimDeadzone * AimDeadzone)
            {
                angle = 0;
                return false;
            }

            float screenAngle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg;
            float camYaw = cam != null ? cam.transform.eulerAngles.y : 0f;
            angle = screenAngle + camYaw;

            return true;
        }
    }
}