namespace LostIsland.Combat
{
    // SyncList<WeaponSlot> icinde kullanildigi icin struct olmak ZORUNDA.
    // class olursa slots[i] ayni referansi dondurur, yerinde degistirilen deger
    // eski degerle "esit" gorunur ve Mirror hicbir zaman guncelleme gondermez.
    public struct WeaponSlot
    {
        public ushort weaponId; // 0 = bos
        public int ammo;
    }
}
