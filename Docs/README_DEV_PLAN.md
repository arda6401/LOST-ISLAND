# LOST ISLAND — Teknik Mimari ve Geliştirme Planı

**Doküman sürümü:** 2.0  
**Motor:** Unity 6 + URP  
**Ağ:** Mirror + KCP Transport  
**Fizik ve simülasyon:** Unity Physics (Authoritative Dedicated Server)  
**Yazılım Mimarisi:** Olay Güdümlü Mimari (Event-Driven Architecture), ScriptableObject Architecture, SOLID  
**Geliştirme yaklaşımı:** Önce decoupled çekirdek sistemler ve oynanabilir dikey kesit (vertical slice), ardından ağ optimizasyonu ve backend

---

## 📌 İçindekiler
1. [Mimari Karar Defteri](#1-mimari-karar-defteri)
2. [Genel Sistem Mimarisi & Event Akışı](#2-genel-sistem-mimarisi--event-akışı)
3. [Olay Güdümlü Mimari (Event-Driven Architecture) Standartları](#3-olay-güdümlü-mimari-event-driven-architecture-standartları)
4. [Sistemler Arası Olay (Event) Matrisi](#4-sistemler-arası-olay-event-matrisi)
5. [Unity Physics & Otoriter Sunucu Mimarisi](#5-unity-physics--otoriter-sunucu-mimarisi)
6. [Mirror Ağ ve Olay Senkronizasyonu](#6-mirror-ağ-ve-olay-senkronizasyonu)
7. [Çatışma, Silah ve Hasar Hattı](#7-çatışma-silah-ve-hasar-hattı)
8. [ScriptableObject Mimarisi ve Veri Odaklı Tasarım](#8-scriptableobject-mimarisi-ve-veri-odaklı-tasarım)
9. [Profesyonel Kodlama & Performans Standartları](#9-profesyonel-kodlama--performans-standartları)
10. [Proje Klasör ve Assembly (asmdef) Yapısı](#10-proje-klasör-ve-assembly-asmdef-yapısı)
11. [Geliştirme Yol Haritası (Milestones)](#11-geliştirme-yol-haritası-milestones)
12. [İlk Prototipin Kapsamı & Kabul Kriterleri](#12-ilk-prototipin-kapsamı--kabul-kriterleri)

---

## 1. Mimari Karar Defteri

| Konu | Karar | Gerekçe / Standart | Durum |
|---|---|---|---|
| **Oyun motoru** | Unity 6 + URP | Modern grafik pipeline'ı, gelişmiş performans ve stabilite. | Kesin |
| **Yazılım Deseni** | Event-Driven & Decoupled (EDA) | Sistemlerin birbirine doğrudan bağımlılığını (tight coupling) engellemek; modülerliği ve test edilebilirliği artırmak. | Kesin |
| **Arayüz (UI) Modeli** | Reaktif / Push-Based (Sıfır Polling) | `Update()` içerisinde sürekli veri sorgulama (polling) kesinlikle yasaktır; UI yalnızca ilgili event tetiklendiğinde güncellenir. | Kesin |
| **Bellek Yönetimi** | Merkezi Object Pooling | Mermiler, kovanlar, efektler ve sesler için sıfır GC tahsisi (Garbage Collection stutter engelleme). | Kesin |
| **Ağ kütüphanesi** | Mirror + KCP | Düşük gecikmeli UDP tabanlı bağlantı, açık kaynak esneklik ve stabil Dedicated Server desteği. | Kesin |
| **Sunucu modeli** | Dedicated Server (Authoritative) | Hile ve desenskronizasyonu önleme; tüm kritik oyun kuralları sunucu onaylıdır. | Kesin |
| **Fizik ve çarpışma** | Unity Physics (XZ düzlemi, Y dikey) | Performanslı 3D çarpışma ve Raycast/SphereCast sorguları. | Kesin |
| **Girdi Sistemi** | Unity New Input System | C# Action ve Event tabanlı, çoklu platforma (PC/Gamepad/Mobil) kolay uyarlanabilir girdi katmanı. | Kesin |
| **Veri Yönetimi** | ScriptableObject Architecture | Kod içine sabit değer (hardcoded) yazılmaması, veri ve mantık ayrımı, tasarımcı dostu modifikasyon. | Kesin |
| **Modülerlik** | Assembly Definitions (asmdef) | Katmanlar arası bağımlılık sınırları çizme ve Unity derleme sürelerini minimize etme. | Kesin |
| **İlk platform** | PC (Klavye + Fare) | Hızlı prototipleme ve test ortamı. | Geçici |
| **İlk prototip ölçeği** | 20 oyuncu/bot | Küçük haritada stabil fizik ve ağ yükü testi. | Hedef |

---

## 2. Genel Sistem Mimarisi & Event Akışı

Oyun; sunucu otoritesini korurken istemci tarafında akıcı bir deneyim sunmak için olay tabanlı yayınla-abone ol (Publish-Subscribe) modelini kullanır.

```text
                                LOST ISLAND ENGINE
                                        |
                 +----------------------+----------------------+
                 |                                             |
           GAME SERVER                                   UNITY CLIENT
        (Dedicated Server)                            (Oyuncu İstemcisi)
                 |                                             |
      [ Otoriter Oyun Mantığı ]                       [ Girdi (New Input) ]
      - Unity Physics Simülasyonu                              |
      - Hasar & Vuruş Onayı                                    v
      - Envanter & Loot Doğrulama                       (Command / Niyet)
      - AURA Zone Hesaplaması                                  |
                 |                                             |
                 +---------------- Mirror / KCP ---------------+
                                        |
                                [ Ağ Senkronizasyonu ]
                                (Network Events & RPCs)
                                        |
                                        v
                          [ İstemci EventBus / SO Events ]
                                        |
         +-----------------+------------+-------------+------------------+
         |                 |                          |                  |
         v                 v                          v                  v
     [ UI / HUD ]     [ SFX Audio ]             [ VFX / FX ]      [ Kamera / Animasyon ]
     - Can Barı       - Ateş & İsabet Sesleri   - Mermi İzi       - Kamera Sarsıntısı
     - Mermi Sayacı   - Kalkan Kırılma Sesi     - Kan / Kıvılcım  - Reaktif Tepkiler
     - Mini Harita    - Çevre Uyarı Sesleri     - Alan Sınırı     - Geri Tepme Hissi
```

### Örnek Olay Akışı: Ateş Etme ve Hasar Döngüsü
```text
[İstemci] Input -> Event: OnFireInputTriggered -> Network: CmdFireWeapon(rayOrigin, aimDirection)
   ↓
[Sunucu]  Komutu Alır -> Silah/Mühimmat Doğrulaması -> Physics.Raycast()
   ↓
[Sunucu]  İsabet Başarılı -> Hasar Uygula -> Sunucu Eventi: ServerOnDamageCalculated
   ↓
[Sunucu]  Ağ Olayı Yayınla: RpcDamageConfirmed(targetId, damageAmount, hitPoint)
   ↓
[İstemciler] İstemci EventBus: Event fırlatılır (OnHitConfirmed)
   ├─► UI HUD: Hasar sayısını ekranda uçur (Damage Number Popup)
   ├─► Ses Yöneticisi: İsabet ses efekti çal (Hit Marker SFX)
   ├─► Efekt Yöneticisi: ObjectPool'dan kan/kıvılcım parçacığı çıkar
   └─► Hedef Oyuncu: HealthComponent -> Event: OnHealthChanged -> Can barını güncelle
```

---

## 3. Olay Güdümlü Mimari (Event-Driven Architecture) Standartları

Sistemler arasındaki tüm iletişimde katı kurallar uygulanır:

### 3.1. Gevşek Bağlılık İlkesi (Loose Coupling)
- Hiçbir oyun bileşeni doğrudan diğer sistemlerin somut sınıflarına bağlanarak onları yönetmez.
- **Kötü Pratik:** `HealthComponent` içinde `public PlayerHUD hud;` tutup `hud.UpdateHealthBar(currentHealth)` çağırmak.
- **Profesyonel Pratik:** `HealthComponent`, durum değiştiğinde `public event Action<float, float> OnHealthChanged;` veya ScriptableObject GameEvent tetikler. `PlayerHUD` bu olayı sahnede dinler ve kendini yeniler.

### 3.2. Sıfır Polling İlkesi (Zero Polling / Push-Based UI)
- `MonoBehaviour.Update()` içerisinde her karede veri durumu kontrol etmek **kesinlikle yasaktır**.
  ```csharp
  // KESİNLİKLE YASAK (Polling / Anti-pattern)
  void Update() {
      healthSlider.value = playerHealth.currentHealth;
      ammoText.text = weapon.currentAmmo.ToString();
  }
  ```
- Bunun yerine tamamen olay dinleme (Subscription) esastır:
  ```csharp
  // PROFESYONEL YÖNTEM (Event-Driven / Push-based)
  private void OnEnable() {
      healthEvent.OnHealthChanged += HandleHealthChanged;
  }
  private void OnDisable() {
      healthEvent.OnHealthChanged -= HandleHealthChanged;
  }
  private void HandleHealthChanged(float current, float max) {
      healthSlider.value = current / max;
  }
  ```

### 3.3. Event Kanalları Türleri ve Kullanım Rehberi

1. **C# Standart Action / Event:**
   - Aynı GameObject veya yakın hiyerarşideki bileşenler arası yüksek performanslı iletişim için kullanılır (örn: `WeaponController` -> `WeaponAudio`).
2. **ScriptableObject Game Events (SO Events):**
   - Farklı sahneler, prefab'lar ve izole alt sistemler arası sahne bağımsız iletişim için kullanılır (örn: `OnPlayerDiedSOEvent`, `OnZoneShrinkSOEvent`). Inspector üzerinden bağlanabilir ve tasarımcı dostudur.
3. **Merkezi EventBus (Pub/Sub):**
   - Küresel oyun akışı, maç durumları ve ağdan gelen küresel bildirimlerin tip güvenli (type-safe) dağıtımı için kullanılır (`EventBus.Publish<MatchStateChangedEvent>(...)`).

---

## 4. Sistemler Arası Olay (Event) Matrisi

Tüm sistemler bu olay sözleşmelerine (contracts) göre inşa edilecektir:

| Sistem | Yayınladığı Temel Olaylar (Publish) | Dinlediği Olaylar (Subscribe) |
|---|---|---|
| **Input System** | `OnMoveInput`, `OnAimInput`, `OnFireInputStarted`, `OnFireInputCanceled`, `OnReloadInput`, `OnAbilityInput` | `OnMatchStateChanged` (Girdileri kilitle/aç) |
| **Player System** | `OnPlayerSpawned`, `OnPlayerStateChanged`, `OnPlayerDied` | `OnHealthChanged`, `OnMoveInput`, `OnAbilityCast` |
| **Combat & Health** | `OnHealthChanged`, `OnShieldChanged`, `OnDamageTaken`, `OnHitConfirmed`, `OnDeath` | `OnWeaponHit`, `OnZoneDamageTick` |
| **Weapon System** | `OnWeaponFired`, `OnReloadStarted`, `OnReloadCompleted`, `OnAmmoChanged`, `OnWeaponSwapped` | `OnFireInputStarted`, `OnReloadInput` |
| **Inventory & Loot** | `OnItemPickedUp`, `OnItemDropped`, `OnInventoryChanged`, `OnChestOpened` | `OnInteractInput`, `OnDeath` (Loot saçma) |
| **Operator System** | `OnAbilityCast`, `OnAbilityCooldownChanged`, `OnAbilityEnded` | `OnAbilityInput`, `OnDeath` (Yetenek kesme) |
| **Zone (AURA)** | `OnZonePhaseChanged`, `OnZoneShrinkStarted`, `OnZoneShrinkEnded`, `OnPlayerEnteredDangerZone`, `OnPlayerExitedDangerZone` | `OnMatchStarted` |
| **Match System** | `OnMatchStateChanged` (Lobby, Countdown, Active, Ended), `OnPlayerCountChanged`, `OnMatchWinnerAnnounced` | `OnPlayerDied`, `OnZonePhaseChanged` |
| **Audio & VFX** | `OnSoundPlayed`, `OnVFXSpawned` | `OnWeaponFired`, `OnHitConfirmed`, `OnAbilityCast`, `OnDamageTaken`, `OnDeath` |
| **UI / HUD** | `OnUIActionTriggered` | `OnHealthChanged`, `OnAmmoChanged`, `OnAbilityCooldownChanged`, `OnZonePhaseChanged`, `OnPlayerCountChanged` |

---

## 5. Unity Physics & Otoriter Sunucu Mimarisi

- **Hareket Düzlemi:** XZ düzlemi. Y ekseni yükseklik ve basamak tırmanma için kullanılır.
- **Sunucu Otoritesi:** İstemci karakter kontrolcüsü doğrudan dünya konumunu yazmaz; giriş vektörünü sunucuya iletir. Sunucu `CharacterController.Move()` veya Rigidbody kuvvetini uygular ve doğrulanmış pozisyonu bildirir.
- **Hitscan ve Çarpışma Doğrulaması:**
  - Silah atışları sunucu tarafında `Physics.Raycast` / `SphereCast` ile hesaplanır.
  - Katmanlar (Layer Mask) optimize edilir: `PlayerHitbox`, `EnvironmentStatic`, `ShieldObstacle`.
  - Engel arkasından atışlar (wallhack vb.) ve gecikmeli vuruşlar sunucu geometrisiyle doğrulanır.
- **Mantık & Fizik Sınırı:** Can, mühimmat, yetenek süresi ve oyun kuralları fizik döngüsünden bağımsız, saf veri ve event odaklı C# bileşenleriyle yürütülür.

---

## 6. Mirror Ağ ve Olay Senkronizasyonu

### Ağ İletişim İlkeleri
- **Komutlar (Commands - `[Command]`):** İstemcinin sunucudan eylem talep etmesi (ateş etme, yetenek açma, sandık açma).
- **Hedefli Olaylar (TargetRpc - `[TargetRpc]`):** Yalnızca ilgili istemciye özel bildirimler (örn. envanter güncellemesi, kişisel hasar bildirimi).
- **İstemci Olayları (ClientRpc - `[ClientRpc]`):** Çevredeki tüm oyuncuların görmesi gereken durumlar (ateş efekti, ses, yetenek patlaması).
- **Durum Senkronizasyonu (SyncVars):** Sürekli değişmeyen kritik değişkenler için (`Hook` fonksiyonları ile event tetiklenir).
  ```csharp
  [SyncVar(hook = nameof(OnHealthChangedHook))]
  private float currentHealth;

  private void OnHealthChangedHook(float oldHealth, float newHealth) {
      // Yerel event fırlatarak HUD ve sesleri bilgilendir
      OnHealthChanged?.Invoke(newHealth, maxHealth);
  }
  ```

---

## 7. Çatışma, Silah ve Hasar Hattı

- **Geri Tepme (Recoil) & Yayılma (Spread):** Silah tanımındaki (ScriptableObject) matematiksel eğrilere göre hesaplanır.
- **Hasar Katmanları:**
  - Vücut Vuruşu (Standart Hasar)
  - Kritik Vuruş (Headshot / Zayıf Nokta Çarpanı)
  - Kalkan Emilimi (Önce zırh/kalkan puanı tükenir, kalan cana geçer).
- **Vuruş Olayı Akışı:**
  1. Sunucu isabeti doğrular.
  2. Hedefin `IDamageable` arayüzü tetiklenir.
  3. `DamageData` yapısı (hasar, vuran, vuruş noktası, hasar türü) event olarak iletilir.
  4. Saldırgana vuruş onay eventi (`OnHitConfirmed`), hedefe hasar alma eventi (`OnDamageTaken`) döner.

---

## 8. ScriptableObject Mimarisi ve Veri Odaklı Tasarım

Tüm tasarım parametreleri koddan tamamen izole edilir:

```text
Assets/_Project/Data/
├── Operators/          # OperatorDefinition (İZ, KİLİT, AKS, EKO)
├── Abilities/          # AbilityDefinition (Bekleme süreleri, menziller, etki verileri)
├── Weapons/            # WeaponDefinition (Hasar, atış oranı, şarjör, sekme eğrisi)
├── Ammo/               # AmmoDefinition (Mühimmat türleri ve taşıma limitleri)
├── LootTables/         # LootTable (Sandık düşme olasılık matrisleri)
├── Zone/               # ZonePhaseTable (Daralma süreleri, yarıçaplar, DPS hasarı)
└── Events/             # ScriptableObject Game Events (Sahneler arası kanallar)
```

**Kural:** Sayısal oyun parametrelerini değiştirmek için kod derlemesi gerekmez; tüm değerler Unity Editor Inspector üzerinden canlı ayarlanabilir.

---

## 9. Profesyonel Kodlama & Performans Standartları

### 9.1. SOLID Prensipleri
- **Single Responsibility (SRP):** Bir sınıf yalnızca tek bir işi yönetir (Örn: `PlayerMovement`, `PlayerCombat`, `PlayerAnimation` ayrı sınıflardır).
- **Open/Closed (OCP):** Yeni operatörler veya yetenekler ana kodu değiştirmeden `AbilityBase` veya `WeaponBase` türetilerek eklenir.
- **Interface Segregation (ISP):** Geniş arayüzler yerine küçük, odaklı arayüzler: `IDamageable`, `IInteractable`, `IHealable`, `IPoolable`.
- **Dependency Inversion (DIP):** Sınıflar somut implementasyonlara değil, soyutlamalara (arayüzlere ve SO Event kanallarına) bağımlı olur.

### 9.2. Sıfır Bellek Tahsisi (Zero GC Allocation) & Object Pooling
- Çalışma anında sık tekrarlanan nesneler için `Instantiate()` ve `Destroy()` kullanımı yasaktır.
- `ObjectPool<T>` mimarisi uygulanır:
  - Mermiler ve Kovanlar
  - Parçacık Efektleri (VFX)
  - Zemin Ayak İzleri ve Mermi Delikleri
  - Yüzen Hasar Metinleri (Damage Popup UI)
  - 3D Ses Kaynakları (AudioSource havuzu)

### 9.3. Durum Makineleri (State Machine)
- **Karakter Durumu:** `Idle`, `Walking`, `Sprinting`, `Aiming`, `CastingAbility`, `Dead` durumları temiz bir `PlayerStateMachine` ile yönetilir.
- **Maç Durumu:** `MatchStateMachine` (WaitingForPlayers, StartingCountdown, InProgress, MatchEnded, Summary).

---

## 10. Proje Klasör ve Assembly (asmdef) Yapısı

```text
LOST ISLAND/
├── Client/
│   └── Assets/
│       └── _Project/
│           ├── Assemblies/                     # Bağımsız derleme birimleri
│           │   ├── LostIsland.Core.asmdef      # EventBus, ObjectPool, Interfaces
│           │   ├── LostIsland.Data.asmdef      # ScriptableObject tanımları
│           │   ├── LostIsland.Gameplay.asmdef  # Player, Combat, Weapons, Loot, Zone
│           │   ├── LostIsland.Network.asmdef   # Mirror network köprüleri
│           │   └── LostIsland.UI.asmdef        # Reaktif HUD ve ekranlar
│           ├── Data/                           # ScriptableObject varlıkları
│           ├── Prefabs/                        # Karakter, silah, mermi, sandık
│           ├── Scenes/                         # Bootstrap, Menu, Prototype_AURA01
│           └── Scripts/
│               ├── Core/
│               │   ├── Events/                 # EventBus, GameEvent ScriptableObjects
│               │   ├── Pooling/                # ObjectPool ve IPoolable
│               │   ├── StateMachine/           # State, StateMachine taban sınıfları
│               │   └── Interfaces/             # IDamageable, IInteractable
│               ├── Data/                       # SO C# sınıfları
│               ├── Player/                     # Karakter hareket, kamera, girdi
│               ├── Combat/                     # Hasar, sağlık, zırh mantığı
│               ├── Weapons/                    # Silah ateşleme, şarjör
│               ├── Operators/                  # Aktif & pasif operatör sınıfları
│               ├── Loot/                       # Sandık, yer eşyası, envanter
│               ├── Zone/                       # AURA güvenli alan çemberi
│               ├── Networking/                 # Mirror mesaj yöneticileri
│               └── UI/                         # Reaktif HUD, menüler
```

---

## 11. Geliştirme Yol Haritası (Milestones)

| Aşama | Odak & Kapsam | Tamamlanma Ölçütü (Definition of Done) |
|---|---|---|
| **M0** | Proje Temeli, Asmdef, Git, Mirror | Unity 6 projesi, temiz katmanlı klasör yapısı ve Mirror hazır. |
| **M1** | **Core Event & Architecture Foundation** | `EventBus`, `ScriptableObject GameEvent` sistemi ve `ObjectPool` devrede. Yeni Input System ile decoupled girdi yayını. |
| **M2** | **Karakter Hareketi & Kamera** | İzometrik kamera ve sunucu otoriteli XZ hareketi. Giriş -> Olay -> Simülasyon döngüsü çalışıyor. |
| **M3** | **Event-Driven Çatışma & Silah** | Hitscan atış, Object Pool ile mermiler. `IDamageable`, `OnHealthChanged`, `OnHitConfirmed` olayları ile çalışan reaktif HUD (Sıfır polling). |
| **M4** | **Loot & Envanter Mekaniği** | Yerden eşya alma, sandık açma (`OnItemPickedUp`, `OnInventoryChanged`). Mühimmat ve silah değiştirme eventleri. |
| **M5** | **İlk Operatör: İZ** | İZ aktif (İz Taraması) ve pasif yeteneği. Yetenek HUD'u ve eventleri (`OnAbilityCast`, `OnAbilityCooldownChanged`). |
| **M6** | **AURA Güvenli Alanı & Maç Döngüsü** | Daralan çember, alan dışı hasar eventi, maç başlangıç ve sonuç state machine'i. |
| **M7** | **AURA-01 Harita Blockout & Ses/VFX** | Modüler test haritası. Ses ve efekt sistemlerinin olaylara tam entegrasyonu. |
| **M8** | **Botlar & Çok Oyunculu Stres Testi** | Dedicated Server üzerinde 20 bot/oyuncu ile event senkronizasyonu ve performans profilleme. |
| **M9** | **İçerik & Denge** | Diğer operatörler (KİLİT, AKS, EKO), yeni silahlar ve son görsel cila. |

---

## 12. İlk Prototipin Kapsamı & Kabul Kriterleri

İlk hedefimiz geniş kapsamlı bir içerik yığını değil; **mimari olarak temiz, event tabanlı ve son derece akıcı bir dikey kesittir (vertical slice).**

### Kabul Kriterleri (DoD - Definition of Done):
1. **Mimari:** Kod tabanında UI veya ses sistemlerinin doğrudan oynanış sınıflarına tight-coupled referans tutmaması; her şeyin eventler üzerinden akması.
2. **Sıfır Polling:** HUD veya herhangi bir arayüz elemanının `Update()` döngüsünde veri sorgulamaması.
3. **Performans:** Çatışma anında hiçbir mermi veya parçacık efekti için `Instantiate/Destroy` çağrılmaması (Object Pooling zorunlu).
4. **Oynanış:** İki istemcinin dedicated server'a bağlanarak izometrik açıda hareket edebilmesi, silah toplayıp birbirine ateş edebilmesi, hasarın ve eleme durumunun reaktif HUD'a kusursuz yansıması.
