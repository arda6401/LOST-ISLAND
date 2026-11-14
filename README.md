# LOST ISLAND

[![Unity Version](https://img.shields.io/badge/Unity-6%20(6000.x)-blue.svg?logo=unity)](https://unity.com/)
[![Render Pipeline](https://img.shields.io/badge/URP-Universal%20Render%20Pipeline-lightgrey.svg)](https://unity.com/srp/Universal-Render-Pipeline)
[![Networking](https://img.shields.io/badge/Networking-Mirror%20%2B%20KCP-orange.svg)](https://mirror-networking.gitbook.io/docs/)
[![Architecture](https://img.shields.io/badge/Model-Dedicated%20Server%20(Authoritative)-green.svg)](#teknik-mimari)
[![Camera](https://img.shields.io/badge/Camera-Isometric%20Orthographic-purple.svg)](#genel-bakış)

> **LOST ISLAND**, yakın gelecekte geçen; oyuncuların maç öncesinde özel yeteneklere sahip operatörler seçtiği, maç sırasında silah ve ekipman topladığı, daralan güvenli alanda hayatta kalmaya çalıştığı **izometrik (orthographic) bir Battle Royale** oyunudur.

---

## 📌 İçindekiler
- [Genel Bakış](#-genel-bakış)
- [Temel Tasarım İlkeleri](#-temel-tasarım-ilkeleri)
- [Evren ve Hikâye (AURA Projesi)](#-evren-ve-hikâye-aura-projesi)
- [Maç Döngüsü ve Oynanış](#-maç-döngüsü-ve-oynanış)
- [Operatör Kadrosu](#-operatör-kadrosu)
- [Silah ve Ganimet (Loot) Sistemi](#-silah-ve-ganimet-loot-sistemi)
- [Harita: AURA-01 Saha İstasyonu](#-harita-aura-01-saha-istasyonu)
- [Teknik Mimari](#-teknik-mimari)
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
| **Modlar** | Solo, Duo, Squad (İlk prototip hedefi: 20 oyuncu/bot) |
| **Maç Süresi** | 10 – 15 Dakika (Hızlı ve taktiksel çatışmalar) |

---

## 🎯 Temel Tasarım İlkeleri

1. **Karakter Kimliği:** Her operatörün aktif ve pasif yetenekleri farklı bir oyun tarzı sunar. Ancak operatörler belirli bir silah sınıfına kilitlenmez; haritada bulunan tüm silahları serbestçe kullanabilirler.
2. **Okunabilir Çatışma:** İzometrik kamera ve temiz stilize görsel dil sayesinde oyuncular; rakipleri, mermileri, ganimetleri ve tehlike alanlarını karmaşa yaşamadan kolayca ayırt eder.
3. **Anlamlı Keşif ve Risk:** Harita bölgeleri farklı ganimet yoğunluğu, görüş açısı ve risk seviyesi sunar.
4. **Adil Rekabet:** Yetenekler bilgi, koruma veya hareket avantajı sağlar; kaçınılmaz ve tek taraflı doğrudan üstünlük üretmez.
5. **Sunucu Otoritesi:** Hile ve desenskronizasyonu engellemek adına hareket, hasar, atış ve ganimet onayları tamamen sunucu kontrolündedir.

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

## ⚙️ Teknik Mimari

Oyun, güvenilir rekabetçi standartları korumak amacıyla **sunucu otoriteli (server-authoritative)** mimariyle inşa edilmektedir.

```text
                 LOST ISLAND
                      |
          +-----------+-----------+
          |                       |
     UNITY CLIENT            GAME SERVER
   Oyuncu İstemcisi       Dedicated Server
          |                       |
    - Girişler (Input)      - Oyun Mantığı & Kural Seti
    - İzometrik Kamera      - Unity Physics (Doğrulama)
    - HUD / UI Sistemi      - Hasar ve İsabet Kontrolü
    - Animasyon & VFX       - Loot ve Envanter Yönetimi
    - SFX / Ses             - AURA Güvenli Alan Çemberi
          |                       |
          +------ Mirror/KCP -----+
                      |
           (Durum Senkronizasyonu)
```

- **Ağ Katmanı:** [Mirror Networking](https://mirror-networking.gitbook.io/docs/) + **KCP Transport** (Düşük gecikmeli UDP tabanlı bağlantı).
- **Fizik Simülasyonu:** Unity Physics (XZ hareket düzlemi, Raycast/SphereCast hitscan vuruş tespiti).
- **Kural:** İstemci eylem talebini iletir (hareket, ateş, loot vb.); nihai durum ve hasar hesaplaması sunucu tarafında yapılır.

---

## 📁 Proje Dizin Yapısı

```bash
LOST ISLAND/
├── Client/                 # Unity 6 Projesi (İstemci & Sunucu kodları)
│   ├── Assets/             # Kodlar, modeller, sahneler, ses ve materyaller
│   │   ├── Scenes/         # Test ve oyun sahneleri (SampleScene vb.)
│   │   └── Settings/       # URP ve Render Pipeline konfigürasyonları
│   ├── Packages/           # Unity paket tanımları (manifest.json)
│   └── ProjectSettings/    # Unity proje, girdi ve kalite ayarları
├── Docs/                   # Detaylı dokümantasyonlar
│   ├── README_LOST ISLAND.md  # Kapsamlı Oyun Tasarımı Dokümanı (GDD)
│   └── README_DEV_PLAN.md     # Teknik Mimari ve Geliştirme Planı
├── .gitignore              # Unity ve IDE için optimize edilmiş kurallar
└── README.md               # Ana proje tanıtımı (bu dosya)
```

---

## 🚀 Geliştirme Yol Haritası

- [x] **Aşama 0:** Konsept, oyun tasarım dokümanı (GDD) ve teknik mimarinin netleştirilmesi.
- [x] **Aşama 1:** Unity 6 + URP proje altyapısının kurulması ve Git repository konfigürasyonu.
- [ ] **Aşama 2 (Core Prototype):** 
  - İzometrik kamera kontrolörü ve karakter hareketi (WASD + Mouse Aim).
  - Temel silah mekaniği (Ateş etme, şarjör doldurma, mermi izleri).
  - Basit zemin ganimeti (yerden silah/mühimmat alma).
- [ ] **Aşama 3 (Networking):**
  - Mirror + KCP entegrasyonu.
  - Dedicated server üzerinde hareket senkronizasyonu ve sunucu taraflı hasar kontrolü.
- [ ] **Aşama 4 (Battle Royale Sistemleri):**
  - AURA daralan alan mekaniği (Zone).
  - Sandık açma, envanter yönetimi ve can/zırh sistemleri.
- [ ] **Aşama 5 (Operatör Yetenekleri & Görselleştirme):**
  - İZ, KİLİT, AKS ve EKO yetenek prototipleri.
  - Stylized 3D çevre modellemeleri ve AURA-01 haritasının blockout'u.

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
