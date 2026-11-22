# LOST ISLAND

[![Unity Version](https://img.shields.io/badge/Unity-6%20(6000.x)-blue.svg?logo=unity)](https://unity.com/)
[![Render Pipeline](https://img.shields.io/badge/URP-Universal%20Render%20Pipeline-lightgrey.svg)](https://unity.com/srp/Universal-Render-Pipeline)
[![Networking](https://img.shields.io/badge/Networking-Mirror%20%2B%20KCP-orange.svg)](https://mirror-networking.gitbook.io/docs/)
[![Architecture](https://img.shields.io/badge/Architecture-Event--Driven%20%2B%20Authoritative-green.svg)](#teknik-mimari)
[![Camera](https://img.shields.io/badge/Camera-Isometric%20Orthographic-purple.svg)](#genel-bakış)

> **LOST ISLAND**, yakın gelecekte geçen; oyuncuların maç öncesinde özel yeteneklere sahip operatörler seçtiği, maç sırasında silah ve ekipman topladığı, daralan güvenli alanda hayatta kalmaya çalıştığı **izometrik (orthographic) bir Battle Royale** oyunudur. Profesyonel oyun geliştirme standartlarına uygun olarak **sunucu otoriteli (server-authoritative)** ve **olay güdümlü (event-driven)** bir mimari üzerine inşa edilmektedir.

---

## 📌 İçindekiler
- [Genel Bakış](#-genel-bakış)
- [Temel Tasarım İlkeleri](#-temel-tasarım-ilkeleri)
- [Evren ve Hikâye (AURA Projesi)](#-evren-ve-hikâye-aura-projesi)
- [Maç Döngüsü ve Oynanış](#-maç-döngüsü-ve-oynanış)
- [Operatör Kadrosu](#-operatör-kadrosu)
- [Silah ve Ganimet (Loot) Sistemi](#-silah-ve-ganimet-loot-sistemi)
- [Harita: AURA-01 Saha İstasyonu](#-harita-aura-01-saha-istasyonu)
- [Teknik Mimari & Yazılım Prensipleri](#-teknik-mimari--yazılım-prensipleri)
- [Proje Dizin Yapısı](#-proje-dizin-yapısı)
- [Geliştirme Yol Haritası](#-geliştirme-yol-haritası)
- [Başlarken ve Kurulum](#-başlarken-ve-kurulum)
- [Detaylı Dokümantasyon](#-detaylı-dokümantasyon)

---

## 🎮 Genel Bakış

| Başlık | Tasarım Kararı |
|---|---|
| **Tür** | Karakter Tabanlı İzometrik Battle Royale |
| **Kamera Açısı** | İzometrik (Orthographic) |
| **Görsel Stil** | Dengeli Stylized 3D |
| **Oyun Motoru** | Unity 6 + URP (Universal Render Pipeline) |
| **Ağ Mimarisi** | Dedicated Server (Sunucu Otoriteli), Mirror + KCP Transport |
| **Yazılım Deseni** | Event-Driven Architecture (EDA), ScriptableObject Architecture, SOLID |
| **Modlar** | Solo, Duo, Squad (İlk prototip hedefi: 20 oyuncu/bot) |
| **Maç Süresi** | 10 – 15 Dakika (Hızlı ve taktiksel çatışmalar) |

---

## 🧪 Prototipte Şu An Neler Var?

Proje şu anda temel çok oyunculu çatışma prototipi aşamasında. Aşağıdakiler kod ve sahnede mevcut; uçtan uca test edilmiş veya tamamlanmış oyun özellikleri olarak değerlendirilmemelidir:

- Mirror tabanlı oyuncu bağlantısı ve komut satırı argümanıyla dedicated server başlatma iskeleti.
- Sunucuya gönderilen hareket ve nişan girdileri; sunucuda karakter hareketi ve mermi çarpışma/hasar hesabı.
- Senkronize can ve ölüm durumu, hasar alınca görsel tepki ve ölüm görseli.
- Yerel oyuncu yaşam döngüsü, ölümden sonra yaşayan oyuncuları izleme ve mobil kontrolleri gizleme.
- Test sahnesinde mobil hareket/nişan kontrolleri.

Henüz uygulanmış maç eşleştirme/backend servisi, çoklu maç sunucu tahsisi, loot/envanter, daralan güvenli alan, operatör yetenekleri veya tamamlanmış maç döngüsü yok. Backend ve çoklu game server taslağı için [teknik plana](Docs/README_DEV_PLAN.md#13-backend-ve-çoklu-game-server-taslağı) bakın.

---

## 🎯 Temel Tasarım İlkeleri

1. **Karakter Kimliği:** Her operatörün aktif ve pasif yetenekleri farklı bir oyun tarzı sunar. Ancak operatörler belirli bir silah sınıfına kilitlenmez; haritada bulunan tüm silahları serbestçe kullanabilirler.
2. **Okunabilir Çatışma:** İzometrik kamera ve temiz stilize görsel dil sayesinde oyuncular; rakipleri, mermileri, ganimetleri ve tehlike alanlarını karmaşa yaşamadan kolayca ayırt eder.
3. **Anlamlı Keşif ve Risk:** Harita bölgeleri farklı ganimet yoğunluğu, görüş açısı ve risk seviyesi sunar.
4. **Adil Rekabet:** Yetenekler bilgi, koruma veya hareket avantajı sağlar; kaçınılmaz ve tek taraflı doğrudan üstünlük üretmez.
5. **Sunucu Otoritesi:** Hile ve desenskronizasyonu engellemek adına hareket, hasar, atış ve ganimet onayları tamamen sunucu kontrolündedir.
6. **Olay Güdümlü & Ayrık Mimari (Event-Driven & Decoupled Architecture):** Sistemler birbirine doğrudan bağımlı (tightly coupled) olmaz. Bütün oyun içi durum değişimleri, etkileşimler ve arayüz güncellemeleri event-driven (Observer, SO Game Events, EventBus) yöntemlerle yönetilir.
7. **Sıfır Polling ve Temiz Kod:** UI, ses ve VFX sistemleri `Update()` içerisinde sürekli değişken kontrolü yapmaz (polling yapılmaz); yalnızca ilgili olay tetiklendiğinde (push-based) reaktif olarak çalışır. Modüler Assembly Definition (asmdef) yapısıyla derleme süreleri optimize edilir ve mimari katman sınırları korunur.

---

## 🌐 Evren ve Hikâye (AURA Projesi)

Yıl **2042**. Dünyanın gözlerden uzak, halka kapalı bir araştırma adasında **AURA** adlı deneysel teknoloji programı yürütülmektedir. Program; insan-makine etkileşimi, enerji savunma kalkanları ve gelişmiş algılama sistemleri üzerine çalışır.

Ana araştırma sistemlerinde yaşanan büyük bir arıza ve arıza sonrası devreye giren otomatik çevreleme sistemi nedeniyle ada dış dünyadan kopar ve karantinaya alınır. Farklı çıkar grupları, adada mahsur kalan değerli AURA verilerini, araştırma prototiplerini ele geçirmek üzere özel operatör ekiplerini adaya indirir. Ancak adanın çevreleme sistemi aktifliğini sürdürmekte ve güvenli alanı adım adım daraltmaktadır.

---

## 🔄 Maç Döngüsü ve Oynanış

```text
[ Operatör Seçimi ] ➔ [ İniş & Başlangıç ] ➔ [ Keşif & Loot ] ➔ [ Çatışma & Alan Daralması ] ➔ [ Final & Tahliye ]
```

1. **Operatör Seçimi:** Maç öncesi taktiksel oyun tarzına uygun karakter seçilir.
2. **İniş & Konum Alma:** Haritada stratejik bir başlangıç noktası belirlenir.
3. **Loot Toplama:** Binalardan ve sandıklardan silah, mühimmat, zırh ve sağlık ekipmanları toplanır.
4. **AURA Çevreleme Alanı:** Güvenli alan kademeli olarak daralır; alan dışında kalanlar çevresel hasar alır.
5. **Final Çatışması:** Hayatta kalan son operatör veya takım bölgeden tahliye hakkını kazanır.

---

## 👥 Operatör Kadrosu

İlk prototip aşamasında 4 temel rolü temsil eden operatörler yer almaktadır:

| Operatör | Rol | Aktif Yetenek | Pasif Yetenek | Renk / Kimlik |
|---|---|---|---|---|
| **İZ** | Keşif / İstihbarat | **İz Taraması:** Çevredeki taze ayak izlerini kısa süreliğine görünür kılar. | **Arazi Okuyucu:** Hareket izlerini diğerlerinden daha uzun süre fark eder. | Haki yeşil, kırık beyaz, koyu gri |
| **KİLİT** | Savunma / Alan Kontrolü | **Enerji Bariyeri:** Yönlendirilebilir, geçici bir koruma kalkanı konuşlandırır. | **Hazırlıklı Ekipman:** Savunma ekipmanlarından daha yüksek verim alır. | Turuncu, koyu gri |
| **AKS** | Hareketlilik / Flank | **Faz Geçişi:** Kısa mesafeli hızlı yer değiştirme (dash) gerçekleştirir. | **Hareket Desteği:** Taban hareket hızında ve manevrada küçük avantaj. | Mavi, beyaz |
| **EKO** | Aldatma / Yanıltma | **Holografik Kopya:** Rakipleri şaşırtmak için kısa süreli sahte operatör yansıtır. | **Düşük İz:** Düşman algılama sistemlerinde daha zor fark edilir. | Mor, camgöbeği |

---

## 🔫 Silah ve Ganimet (Loot) Sistemi

### Silah Kategorileri
- **Saldırı Tüfeği (AR):** Orta menzilde istikrarlı, dengeli hasar.
- **Pompalı Tüfek (Shotgun):** Yakın temas çatışmalarında yıkıcı güç.
- **Hafif Makineli (SMG):** Yakın mesafede yüksek atış hızı ve hareketlilik.
- **Keskin Nişancı Tüfeği (Sniper):** Uzun mesafede yüksek isabet ve kritik hasar.
- **Hafif Makineli Tüfek (LMG):** Alan baskısı ve uzun süreli sürekli ateş gücü.

### Ganimet Sandıkları
- **Standart Sandık (Yeşil/Ahşap):** Temel silahlar, mühimmat ve temel sağlık eşyaları.
- **Teknoloji Sandığı (Mavi):** Gelişmiş silahlar ve daha yüksek seviye koruma plakaları.
- **Özel AURA Kasası (Turuncu):** Nadir prototipler, yüksek değerli ekipmanlar ve özel eşyalar.

---

## 🗺️ Harita: AURA-01 Saha İstasyonu

İlk prototip alanı yaklaşık **300 × 300 metre** boyutlarında bir test adasıdır:

```text
                         KUZEY
        +----------------+----------------+
        |                |                |
        |    ÇAMLIK      |  GÖZLEM        |
        | (Doğal Siper)  |  NOKTASI       |
        +----------------+----------------+
        |                |                |
        |  SERVİS ALANI  |  AURA MERKEZİ  |
        |  (Dar Koridor) |  (Yüksek Loot) |
        +----------------+----------------+
        |                |                |
        |    YERLEŞİM    | ENERJİ BAHÇESİ |
        | (İç Mekânlar)  | (Açık Alanlar) |
        +----------------+----------------+
                         GÜNEY
```

---

## ⚙️ Teknik Mimari & Yazılım Prensipleri

LOST ISLAND, yüksek rekabetçi adalet, yüksek sunucu performansı ve bakım kolaylığı sağlamak için **Sunucu Otoriteli (Server-Authoritative)** ve **Olay Güdümlü (Event-Driven)** bir yazılım mimarisini benimser.

```text
                     LOST ISLAND ENGINE
                             |
             +---------------+---------------+
             |                               |
       UNITY CLIENT                     GAME SERVER
     (Oyuncu İstemcisi)              (Dedicated Server)
             |                               |
    - Girişler (New Input System)   - Otoriter Oyun Mantığı & Kurallar
    - İzometrik Kamera Takibi        - Unity Physics (Doğrulama & Raycast)
    - Reaktif UI / HUD               - Hasar, Vuruş & Eleme Hesaplaması
    - Görsel Efektler (VFX)          - Loot & Envanter Sunucu Doğrulaması
    - Ses Sistemi (SFX)              - AURA Çevreleme Alanı Yönetimi
             |                               |
             +----------- Mirror/KCP --------+
                             |
                  [ AĞ VE DURUM SENKRONİZASYONU ]
                             |
             +---------------+---------------+
             |                               |
    [ Client EventBus / SO Events ]  [ Server Game Events ]
             |                               |
    +--------+--------+              +-------+-------+
    |        |        |              |       |       |
  UI/HUD    SFX      VFX            Zone   Combat  Inventory
 (Reaktif (Sesler) (Parçacık)      (Alan) (Hasar)  (Eşyalar)
  Panel)
```

### 💎 Temel Profesyonel Yöntemler ve Event-Driven Yaklaşım

1. **Gevşek Bağlılık ve Olay Güdümlü Sistemler (Decoupling & Event-Driven Architecture):**
   - Bileşenler asla birbirinin iç detaylarına doğrudan bağlanmaz (tight coupling yasaktır).
   - Örneğin; `HealthComponent` doğrudan `PlayerHUD` referansı tutmaz veya `hud.UpdateHealthBar()` çağırmaz. Bunun yerine `OnHealthChanged` olayını yayınlar (publish eder).
   - UI, ses yöneticisi, haptik/kamera sarsıntısı ve görsel efekt sistemleri bu olaya bağımsızca abone olur (subscribe).

2. **Sıfır Polling (Push-Based UI):**
   - HUD ve arayüz elemanları `Update()` döngüsünde her karede `player.CurrentHealth` veya `player.Ammo` değerini sorgulamaz (polling yapılmaz).
   - Ekran yalnızca ilgili veri değiştiğinde ve bir event fırlatıldığında anlık olarak güncellenir. Bu, mobil ve PC platformlarında sıfır CPU israfı ve yüksek kare hızı sağlar.

3. **ScriptableObject Mimarisi & Veri Odaklı Tasarım (Data-Driven Architecture):**
   - Sayısal değişkenler (hasar, hız, menzil, bekleme süreleri) C# sınıfları içerisine sabit kodlanmaz (hardcoded yapılmaz).
   - `OperatorDefinition`, `WeaponDefinition`, `LootTable`, `ZonePhaseTable` gibi ScriptableObject yapılarıyla oyun tasarımcılarının kodu değiştirmeden denge ayarlaması yapması sağlanır.
   - ScriptableObject tabanlı `GameEvent` mimarisiyle sahneler ve prefab'lar arasında sahne hiyerarşisinden bağımsız event akışı kurulur.

4. **Nesne Havuzlama (Object Pooling):**
   - Mermiler, mermi kovanları, vuruş partikülleri, ayak izleri, ses kaynakları ve yüzen hasar metinleri çalışma zamanında sürekli `Instantiate` ve `Destroy` edilmez.
   - Merkezi bir `ObjectPool` sistemiyle önceden üretilir ve geri dönüştürülür. Çöp toplayıcı (Garbage Collector) duraklamaları ve bellek dalgalanmaları tamamen engellenir.

5. **Modüler Assembly Definition (asmdef) Standartları:**
   - Kod tabanı izole katmanlara ayrılır (`Core.Events`, `Core.Pooling`, `Gameplay.Combat`, `Gameplay.Player`, `UI`, `Networking`).
   - Sirküler bağımlılıklar (circular dependencies) engellenir ve Unity derleme süreleri ciddi oranda kısalır.

6. **Sunucu Otoritesi & Network Olayları:**
   - İstemci yalnızca niyet bildirir (Input/Command). Fiziksel çarpışma, atış isabeti, hasar hesaplaması, eşya alma ve oyuncu eleme kararlarını sunucu verir.
   - Sunucu, durumu onayladığında bunu ilgili istemcilere güvenilir ağ olayları (TargetRpc, ClientRpc veya NetworkMessage) ile bildirir.

---

## 📁 Proje Dizin Yapısı

```bash
LOST ISLAND/
├── Client/                     # Unity 6 Projesi (Client & Dedicated Server)
│   ├── Assets/
│   │   └── _Project/           # Projeye özel ayrılmış ana çalışma dizini
│   │       ├── Assemblies/     # Modüler Asmdef tanımları
│   │       ├── Data/           # ScriptableObject veri varlıkları (SO Events, Tanımlar)
│   │       ├── Prefabs/        # Karakter, silah, mermi, sandık ve UI prefab'ları
│   │       ├── Scenes/         # Bootstrap, Menü, AURA-01 Harita sahneleri
│   │       └── Scripts/        # Profesyonel C# Kod Tabanı
│   │           ├── Core/       # EventBus, ObjectPool, StateMachine, Interfaces
│   │           │   ├── Events/ # SO Game Events ve C# Event altyapısı
│   │           │   └── Pool/   # Merkezi Object Pool sistemi
│   │           ├── Networking/ # Mirror & KCP ağ köprüsü, senkronizasyon yöneticileri
│   │           ├── Player/     # Hareket, kamera takip, girdi dinleyicileri
│   │           ├── Combat/     # Hasar, isabet tespiti, can ve kalkan bileşenleri
│   │           ├── Weapons/    # Silah mekanikleri, ateşleme mantığı
│   │           ├── Operators/  # Aktif ve pasif yetenek sınıfları
│   │           ├── Loot/       # Yer eşyaları, sandıklar, envanter yönetimi
│   │           ├── Zone/       # AURA güvenli alan çember kontrolörü
│   │           └── UI/         # Reaktif, Event-Driven HUD ve menü denetleyicileri
│   ├── Packages/               # Unity paket tanımları (manifest.json)
│   └── ProjectSettings/        # Proje, girdi, fizik ve kalite ayarları
├── Docs/                       # Detaylı mimari ve tasarım dokümantasyonları
│   ├── README_LOST ISLAND.md   # Kapsamlı Oyun Tasarımı Dokümanı (GDD)
│   └── README_DEV_PLAN.md      # Teknik Mimari ve Geliştirme Planı
├── .gitignore                  # Unity ve IDE için optimize edilmiş kurallar
└── README.md                   # Ana proje tanıtımı (bu dosya)
```

---

## 🚀 Geliştirme Yol Haritası

- [x] **Aşama 0:** Konsept, oyun tasarım dokümanı (GDD) ve teknik mimarinin netleştirilmesi.
- [x] **Aşama 1:** Unity 6 + URP proje altyapısının kurulması ve Git repository konfigürasyonu.
- [x] **Aşama 2 (Temel Oyuncu Prototipi):** İzometrik kamera takibi, New Input System girdisi, sunucu otoriteli temel hareket ve nişan.
- [x] **Aşama 3 (İlk Çatışma Prototipi):** Sunucu tarafı mermi çarpışması/hasar, senkronize can ve ölüm, temel vuruş/ölüm görselleri.
- [~] **Aşama 4 (Çok Oyunculu Prototip):** Mirror bağlantısı ve dedicated server başlangıç iskeleti mevcut; maç yaşam döngüsü, hata dayanıklılığı ve hedef oyuncu sayısı doğrulanmayı bekliyor.
- [ ] **Aşama 5 (Tam Maç Döngüsü):** Lobi/maç durumları, loot ve envanter, AURA güvenli alanı, sonuç akışı.
- [ ] **Aşama 6 (Backend ve Çoklu Game Server):** Eşleştirme, sunucu tahsisi, bağlantı bileti ve maç sonucu kaydı. Tasarım taslağı teknik planda.
- [ ] **Aşama 7 (İçerik ve Denge):** AURA-01 haritası, operatör yetenekleri, içerik ve denge çalışması.

---

## 💻 Başlarken ve Kurulum

### Gereksinimler
- **Unity Hub** & **Unity 6 (6000.x)** (URP desteği ile birlikte)
- **Git** & **Git LFS**
- **Visual Studio 2022** / **Visual Studio Code** veya **JetBrains Rider**

### Projeyi Açma
1. Depoyu klonlayın:
   ```bash
   git clone https://github.com/arda6401/LOST-ISLAND.git
   ```
2. Unity Hub'ı açın ve **Add project from disk** seçeneği ile `Client` klasörünü seçin.
3. Unity 6 sürümüyle projeyi başlatın.

---

## 📚 Detaylı Dokümantasyon

Tasarım kararları ve teknik ayrıntılar hakkında daha fazla bilgi için `Docs` klasörünü inceleyebilirsiniz:
- 📖 [Oyun Tasarımı Dokümanı (GDD)](Docs/README_LOST%20ISLAND.md)
- 🛠️ [Teknik Mimari ve Geliştirme Planı](Docs/README_DEV_PLAN.md)
