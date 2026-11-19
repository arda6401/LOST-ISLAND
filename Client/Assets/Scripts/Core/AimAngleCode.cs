using UnityEngine;


namespace LostIsland.Core
{


    public static class AimAngleCode
    {
        private const float Step = 360 / 65536f;


        public static ushort Encode(float degrees)
        {
            float wrapped = Mathf.Repeat(degrees, 360f);
            return (ushort)(Mathf.RoundToInt(wrapped / Step) & 0xFFFF);

        }

        public static float Decode(ushort value)
        {
            return value * Step;
        }
    }
}
