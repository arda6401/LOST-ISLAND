using UnityEngine;


namespace LostIsland.Core
{


    public static class AimAngleCode
    {
        private const float Step = 360 / 65536f;


        public static ushort Encode(float degress)
        {
            float wrapped = Mathf.Repeat(degress, 360f);
            return (ushort)(Mathf.RoundToInt(wrapped / Step) & 0xFFFF);

        }

        public static float Decode(ushort value)
        {
            return value * Step;
        }
    }
}
