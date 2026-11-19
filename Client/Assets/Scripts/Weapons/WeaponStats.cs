namespace LostIsland.Weapons
{
    public struct WeaponStats
    {
        public float FireInterval;
        public float Range;
        public int Damage;
        public float ProjectileSpeed;
        public float ProjectileRadius;

        public static WeaponStats From(WeaponDefinition d) => new WeaponStats
        {
            FireInterval = d.fireInterval,
            Range = d.range,
            Damage = d.damage,
            ProjectileSpeed = d.projectileSpeed,
            ProjectileRadius = d.projectileRadius
        };
    }
}
