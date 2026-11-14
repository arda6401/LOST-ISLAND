# LOST ISLAND — Teknik Mimari ve Geliştirme Planı

**Doküman sürümü:** 1.0  
**Motor:** Unity 6 + URP  
**Ağ:** Mirror + KCP Transport  
**Fizik ve simülasyon:** Unity Physics  
**Sunucu modeli:** Dedicated Server, sunucu otoriteli  
**Geliştirme yaklaşımı:** Önce oynanabilir prototip, sonra optimizasyon ve backend

---

## 1. Mimari karar defteri

| Konu | Karar | Durum |
|---|---|---|
| Oyun motoru | Unity 6 + URP | Kesin |
| Ağ kütüphanesi | Mirror + KCP | Kesin |
| Sunucu modeli | Dedicated Server | Kesin |
| Otorite | Sunucu otoriteli | Kesin |
| Fizik ve çarpışma | Unity Physics | Kesin |
| Fizik düzlemi | XZ, Y dikey eksen | Kesin |
| Fizik tick'i | Başlangıç hedefi 30 Hz | Test edilecek |
| Ağ güncellemesi | Başlangıç hedefi 20 Hz | Test edilecek |
| Vuruş sistemi | Sunucu taraflı Physics.Raycast / SphereCast | Kesin |
| Backend | İlk prototipte yok | Kesin |
| İlk platform | PC geliştirme ortamı | Geçici |
| İlk prototip ölçeği | 20 oyuncu, botlar dahil | Hedef |
| Yeniden doğma | Yok; elenen oyuncu izleyici olur | Kesin |

Başlangıç değerleri performans ve oynanış testleriyle değiştirilebilir. Henüz test edilmemiş değerler kesin performans garantisi değildir.

## 2. Genel sistem mimarisi

```text
                 LOST ISLAND
                      |
          +-----------+-----------+
          |                       |
    UNITY CLIENT             GAME SERVER
    Oyuncu bilgisayarı       Dedicated Server
          |                       |
    - Input                  - Oyun kuralları
    - Kamera                 - Unity Physics
    - HUD                    - Hareket ve çarpışma
    - Animasyon              - Hasar ve silahlar
    - Ses / VFX              - Loot ve envanter
    - Görsel efektler        - Operatör yetenekleri
          |                       |
          +------ Mirror/KCP -----+
                      |
                 Oyun durumu
```

İstemci oyuncunun girdilerini sunucuya gönderir. Sunucu bu girdileri kontrol eder, fizik simülasyonunu yürütür ve doğru oyun durumunu belirler.

**Temel kural:** İstemci hareket etmek istediğini söyler. Gerçek konum, hasar, mühimmat, loot ve maç sonucu hakkında son kararı sunucu verir.

## 3. Unity Physics mimarisi

Hareket, çarpışma ve fiziksel etkileşimler Unity'nin fizik sistemiyle gerçekleştirilecek.

### Oyuncu hareketi

- Karakterler XZ düzleminde hareket eder.
- Y ekseni yükseklik için kullanılır.
- İlk sürümde tek katlı harita hedeflenir.
- Oyuncu hareketi sunucu tarafından yürütülür.
- Duvar, ağaç, konteyner ve diğer engeller Unity Collider bileşenleriyle tanımlanır.
- Hareket için başlangıçta CharacterController değerlendirilir. Gerekli durumlarda Rigidbody tabanlı çözüme geçilebilir.

### Çarpışmalar

- Statik çevre: BoxCollider, CapsuleCollider ve uygun diğer Collider türleri.
- Oyuncular: CharacterController veya uygun Collider.
- Dinamik yetenek nesneleri: Collider ve gerektiğinde Rigidbody.
- KİLİT'in bariyeri gibi nesneler çalışma sırasında oluşturulabilir.
- Fizik katmanları gereksiz çarpışmaları engellemek için düzenlenir.

### Önemli sınır

Unity Physics kullanılması, bütün oyun mantığının fizik motoruna bırakılması anlamına gelmez.

Can, mühimmat, silah bekleme süresi, yetenek bekleme süresi, loot ve maç kuralları normal C# oyun koduyla yönetilir. Unity Physics yalnızca fiziksel hareket, çarpışma ve sorgulama gereken yerlerde kullanılır.

## 4. Mirror ağ mimarisi

### İstemci sorumlulukları

- Oyuncu girdisini toplamak.
- Hareket ve nişan girdilerini göndermek.
- Kamera, HUD ve arayüzü yönetmek.
- Animasyon, ses ve görsel efektleri çalıştırmak.
- Sunucudan gelen oyun durumunu göstermek.

### Sunucu sorumlulukları

- Oyuncu hareketini yürütmek.
- Fizik çarpışmalarını doğrulamak.
- Silah atışlarını ve isabetleri hesaplamak.
- Hasar ve eleme işlemlerini yürütmek.
- Loot ve envanteri yönetmek.
- Operatör yeteneklerini doğrulamak.
- AURA güvenli alanını yönetmek.
- Maçın başlangıç ve bitiş kararlarını vermek.

### Ağ iletişimi

Mirror + KCP kullanılacak.

- Oyuncu girdileri: İstemciden sunucuya.
- Oyuncu durumları: Sunucudan ilgili istemcilere.
- Kritik olaylar: Ölüm, eşya alma, maç durumu gibi olaylar güvenilir iletişimle.
- Sık güncellenen hareket bilgileri: Uygun olduğunda güvenilir olmayan güncellemelerle.
- Görünürlük: İlerleyen aşamada AOI ile sınırlandırılacak.

### Hareket senkronizasyonu

İlk aşamada sunucu otoriteli hareket ve uzak oyuncuların sunucu durumuna göre gösterilmesi yeterlidir.

Client-side prediction ve server reconciliation ilk günden zorunlu tutulmayacak. Gecikme testleri sonucunda ihtiyaç görülürse yerel oyuncu hareketine tahmin ve düzeltme eklenecek.

Bu yaklaşım, ilk prototipte gereksiz ağ karmaşıklığını azaltır.

## 5. Çatışma ve silah sistemi

İlk sürümde hitscan tabanlı silah sistemi kullanılacak.

Atış akışı:

1. İstemci ateş girdisini gönderir.
2. Sunucu oyuncunun hayatta olup olmadığını kontrol eder.
3. Silahın ateş edebilir durumda olup olmadığını kontrol eder.
4. Mühimmat ve ateş aralığını doğrular.
5. Unity Physics ile ışın veya kalın ışın sorgusu gerçekleştirir.
6. Duvar ve engel kontrollerini yapar.
7. İsabet varsa hasarı sunucu uygular.
8. İlgili istemcilere ateş ve hasar olaylarını gönderir.

İlk sürümde gelişmiş lag compensation zorunlu değildir. Önce temel çatışma sistemi çalıştırılır; internet testlerinde adalet sorunu görülürse geçmiş oyuncu konumlarını saklayan bir sistem eklenir.

## 6. Oyun sistemleri

Oyun sistemleri ayrı sorumluluklara sahip bileşenler olarak geliştirilecek.

| Sistem | Sorumluluk |
|---|---|
| Player System | Oyuncu durumu ve hareket |
| Combat System | Ateş, isabet ve hasar |
| Weapon System | Silah ve mühimmat |
| Inventory System | Eşya ve ekipman |
| Loot System | Yerdeki eşyalar ve sandıklar |
| Operator System | Aktif ve pasif yetenekler |
| Zone System | AURA güvenli alanı |
| Match System | Maç aşamaları ve sonuç |
| Bot System | Test amaçlı yapay oyuncular |

Her sistemin oyun kurallarına ilişkin kararları sunucuda verilir.

## 7. ScriptableObject ve oyun verileri

Denge değerleri kodun içine sabit yazılmayacak.

Kullanılacak temel veri varlıkları:

- OperatorDefinition
- AbilityDefinition
- WeaponDefinition
- AmmoDefinition
- ItemDefinition
- LootTable
- ZonePhaseTable
- MatchSettings
- MoveParams

ScriptableObject'ler tasarım ve ayar verilerini tutar. Çalışma sırasında oyun sistemleri bu verileri okur.

İstemci ve sunucu arasında önemli yapılandırma farklılıklarını yakalamak için ileride konfigürasyon sürümü veya hash kontrolü eklenebilir.

## 8. Backend yaklaşımı

Backend ilk prototipin parçası olmayacak.

İlk bağlantı yöntemi doğrudan IP üzerinden olacak. Böylece hesap sistemi, eşleştirme, veritabanı ve sunucu yönetimi geliştirmeyi geciktirmeyecek.

Backend daha sonra şu görevleri üstlenecek:

- Hesap ve oyuncu kimliği.
- Lobi ve eşleştirme.
- Oyun sunucusu başlatma ve yönetme.
- Maç sonuçlarını kaydetme.
- Gerekirse kalıcı oyuncu verileri.

Backend, Unity fizik simülasyonunu çalıştırmayacak. Fizik simülasyonunun tek sahibi oyun sunucusudur.

## 9. Proje klasör yapısı

```text
LOST-ISLAND/
│
├── Client/
│   └── UnityProject/
│       ├── Assets/
│       │   └── _Project/
│       │       ├── Art/
│       │       ├── Audio/
│       │       ├── Data/
│       │       ├── Materials/
│       │       ├── Prefabs/
│       │       ├── Scenes/
│       │       ├── Scripts/
│       │       │   ├── Core/
│       │       │   ├── Networking/
│       │       │   ├── Player/
│       │       │   ├── Combat/
│       │       │   ├── Weapons/
│       │       │   ├── Inventory/
│       │       │   ├── Loot/
│       │       │   ├── Operators/
│       │       │   ├── Zone/
│       │       │   ├── Match/
│       │       │   ├── Bots/
│       │       │   ├── UI/
│       │       │   └── Editor/
│       │       └── Settings/
│       ├── Packages/
│       └── ProjectSettings/
│
├── Backend/
│   └── (ileride)
│
├── Tests/
│
├── Tools/
│
├── Docs/
│   ├── README_LOST_ISLAND.md
│   └── README_DEV_PLAN.md
│
└── README.md
```

İstemci ve dedicated server aynı Unity projesinden build alınarak üretilecek. Böylece iki ayrı oyun projesinin bakım yükü oluşmayacak.

## 10. Geliştirme yol haritası

| Aşama | İçerik | Tamamlanma ölçütü |
|---|---|---|
| M0 | Unity projesi, Git, Mirror kurulumu | Proje ve test sahnesi hazır |
| M1 | Dedicated Server ve oyuncu hareketi | İki istemci aynı sunucuda hareket edebiliyor |
| M2 | Çatışma | İki oyuncu birbirine ateş edip hasar verebiliyor |
| M3 | Loot ve envanter | Eşya alma, silah değiştirme ve mühimmat |
| M4 | İlk operatör: İZ | Aktif ve pasif yetenek çalışıyor |
| M5 | Maç döngüsü | Başlangıç, AURA daralması, eleme ve sonuç |
| M6 | AURA-01 haritası | Blockout harita ve çevresel engeller |
| M7 | Botlar ve test | Botlarla tek başına maç testi |
| M8 | Backend ve internet testi | Lobi, bağlantı ve gerçek oyuncu testi |
| M9 | İçerik üretimi | Modeller, animasyonlar, ses ve yeni operatörler |

Milestone süreleri, okul ve diğer sorumluluklarla birlikte gerçek geliştirme hızına göre belirlenecek. Takvim tahminleri kesin teslim tarihi değildir.

## 11. İlk prototipin kapsamı

İlk hedef tam oyun değil, temel oynanışın çalıştığı küçük bir sürümdür.

İçerik:

- İzometrik kamera.
- Oyuncu hareketi.
- Unity Physics çarpışmaları.
- Mirror bağlantısı.
- Dedicated Server.
- Bir saldırı tüfeği.
- Can ve hasar.
- Yerde eşya alma.
- Basit envanter.
- Bir sandık.
- İZ operatörü.
- Basit AURA güvenli alanı.
- Küçük test haritası.
- Maç başlangıcı ve bitişi.

İlk prototipte ertelenecekler:

- Tam hesap ve arkadaş sistemi.
- Eşleştirme altyapısı.
- Gelişmiş lag compensation.
- Karmaşık envanter ve silah eklentileri.
- Çok katlı binalar.
- Gelişmiş görüş/duvar arkası filtreleri.
- Çok sayıda operatör.
- Kozmetik ve mağaza sistemi.

## 12. Teknik öncelik

Her aşamada önce çalışan ve test edilebilen bir sistem üretilecek.

Bir sistemin tamamlanma ölçütü yalnızca kodun yazılmış olması değil, Unity içinde test edilmiş ve diğer sistemlerle çalışıyor olmasıdır.

**İlk teknik hedef: İki istemci, bir dedicated server ve Unity Physics ile çalışan oyuncu hareketi.**