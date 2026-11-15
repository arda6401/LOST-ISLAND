# LOST ISLAND

> **Proje türü:** Karakter tabanlı Battle Royale  
> **Görsel stil:** Dengeli Stylized 3D  
> **Kamera:** İzometrik (Orthographic)  
> **Oyun motoru:** Unity 6 + URP  
> **Doküman sürümü:** 0.3  
> **Durum:** Konsept ve ön üretim taslağı

LOST ISLAND, yakın gelecekte geçen; oyuncuların maç öncesinde özel yeteneklere sahip operatörler seçtiği, maç sırasında silah ve ekipman topladığı, daralan savaş alanında hayatta kalmaya çalıştığı izometrik bir Battle Royale oyunudur.

Bu README, şu ana kadar belirlenen evreni, temel oynanışı, karakterleri, haritayı, sanat yönetimini ve geliştirme hedeflerini tek yerde toplar. Henüz test edilmemiş sayısal değerler **öneri** veya **prototip hedefi** olarak işaretlenmiştir.

---

## İçindekiler

1. [Oyunun kısa özeti](#1-oyunun-kısa-özeti)
2. [Temel tasarım hedefleri](#2-temel-tasarım-hedefleri)
3. [Evren ve hikâye](#3-evren-ve-hikâye)
4. [Oyuncunun amacı](#4-oyuncunun-amacı)
5. [Maç akışı](#5-maç-akışı)
6. [Battle Royale kuralları](#6-battle-royale-kuralları)
7. [Operatör sistemi](#7-operatör-sistemi)
8. [İlk operatör kadrosu](#8-ilk-operatör-kadrosu)
9. [Silah ve ekipman sistemi](#9-silah-ve-ekipman-sistemi)
10. [Ganimet (loot) sistemi](#10-ganimet-loot-sistemi)
11. [Güvenli alan ve AURA sistemi](#11-güvenli-alan-ve-aura-sistemi)
12. [İlk harita: AURA-01](#12-ilk-harita-aura-01)
13. [Sanat yönetimi](#13-sanat-yönetimi)
14. [Kamera ve okunabilirlik](#14-kamera-ve-okunabilirlik)
15. [Animasyon, ses ve görsel efektler](#15-animasyon-ses-ve-görsel-efektler)
16. [Arayüz ve kullanıcı deneyimi](#16-arayüz-ve-kullanıcı-deneyimi)
17. [Teknik yön ve Unity yapısı](#17-teknik-yön-ve-unity-yapısı)
18. [İlk prototip kapsamı](#18-ilk-prototip-kapsamı)
19. [Üretim yol haritası](#19-üretim-yol-haritası)
20. [Henüz kararlaştırılmamış konular](#20-henüz-kararlaştırılmamış-konular)
21. [Terimler sözlüğü](#21-terimler-sözlüğü)

---

## 1. Oyunun kısa özeti

**LOST ISLAND**, oyuncuların AURA adlı deneysel teknoloji programının yürütüldüğü karantinaya alınmış bir adaya gönderildiği karakter tabanlı bir Battle Royale oyunudur.

Oyuncu maç başlamadan önce bir operatör seçer. Her operatörün kendine özgü bir aktif ve pasif yeteneği vardır. Ancak operatörler belirli silahlara bağlı değildir: Oyuncular maç sırasında haritayı keşfederek silah, mühimmat, koruma ve diğer ekipmanları bulur.

Maç ilerledikçe AURA tesisinin çevreleme sistemi güvenli bölgeyi daraltır. Oyuncular daha küçük bir alanda karşı karşıya gelir. Son hayatta kalan oyuncu veya takım, AURA bölgesinden çıkış hakkını kazanır.

### Temel bilgiler

| Başlık | Şu anki tasarım kararı |
|---|---|
| Tür | Karakter tabanlı Battle Royale |
| Kamera | İzometrik, Orthographic |
| Görsel stil | Dengeli Stylized 3D |
| Dünya | Yakın gelecek; cyberpunk olmayan teknoloji estetiği |
| Karakterler | Maç öncesinde seçilen, aktif ve pasif yetenekli operatörler |
| Silahlar | Maç sırasında bulunan, karakterden bağımsız silahlar |
| Modlar | Solo, Duo ve Squad planlanıyor |
| İlk test haritası | AURA-01: Saha İstasyonu |
| Motor | Unity 6, URP |

### Hedeflenen oyun hissi

- Hızlı ve anlaşılır çatışmalar.
- Her maçta farklı silah ve ekipman kombinasyonları.
- Karakter seçiminin maç stratejisini etkilemesi.
- Keşif, ganimet toplama, pozisyon alma ve hayatta kalmanın dengeli olması.
- Renkli ve stilize görünmesine rağmen rekabetçi okunabilirliğin korunması.

---

## 2. Temel tasarım hedefleri

LOST ISLAND'ın tasarımında beş temel ilke bulunur:

1. **Karakter kimliği:** Her operatör farklı bir oyun tarzı sunmalı; ancak hiçbiri belirli bir silahı kullanmak zorunda olmamalı.
2. **Maç çeşitliliği:** Silahlar, ekipmanlar ve ganimet konumları maçtan maça değişebilmeli.
3. **Okunabilir çatışma:** İzometrik kamera ve stilize görsel dil, oyuncunun rakibi, mermiyi, ganimeti ve çevresel tehlikeleri kolayca ayırt etmesini sağlamalı.
4. **Anlamlı keşif:** Haritanın bölgeleri farklı risk ve ödül seviyeleri sunmalı.
5. **Adil rekabet:** Yetenekler bilgi veya hareket avantajı sağlayabilir; doğrudan ve kaçınılmaz üstünlük yaratmamalı.

---

## 3. Evren ve hikâye

### Zaman ve ortam

Hikâye **2042 yılında** geçer. Dünya, enerji üretimi, otonom sistemler, savunma teknolojileri ve insan performansını destekleyen cihazlar alanında önemli ilerlemeler kaydetmiştir.

Dünyanın gözlerden uzak bir bölgesinde, halka kapalı bir araştırma adası bulunur. Bu adada **AURA** adı verilen deneysel teknoloji programı yürütülmektedir. Program; gelişmiş algılama sistemleri, enerji tabanlı savunma ekipmanları ve insan-makine etkileşimini geliştiren sistemler üzerinde çalışır.

### AURA olayı

Bir gün adadaki ana araştırma sistemlerinde büyük bir arıza meydana gelir. Tesislerin güvenlik ve kontrol ağları kesintiye uğrar. Bazı deneysel sistemler kontrol dışı kalır ve adanın dış dünyayla bağlantısı kesilir.

Olayın ardından ada karantinaya alınır. Resmî açıklamalar sınırlı kalırken AURA programına ait araştırma verileri, prototip cihazlar ve sistem kayıtları farklı grupların ilgisini çekmeye başlar.

Zamanla adaya, farklı çıkarlar için çalışan özel operatör ekipleri gönderilir. Bu operatörlerin ortak noktası, adadaki değerli AURA verilerine ulaşmak ve bölgeden çıkabilmektir. Ancak adanın çevreleme sistemi hâlâ çalışmaktadır ve güvenli hareket alanı giderek daralmaktadır.

### Hikâyenin oyuncuyla bağlantısı

Oyuncu, AURA adasına gönderilen operatörlerden birini kontrol eder. Operatörün amacı adadaki operasyonu tamamlamak, hayatta kalmak ve tahliye hakkını elde etmektir.

**Hikâyenin ana sorusu:** AURA'da gerçekte ne yaşandı ve sistemin kontrolünü kim kaybetti?

Bu sorunun cevabı oyun dünyasına yerleştirilen kayıtlar, tesis belgeleri, çevresel detaylar ve ileride eklenebilecek sezon içerikleriyle açılabilir.

> **Hikâye notu:** Bu bölüm projenin mevcut hikâye taslağıdır. AURA olayının kesin nedeni, sorumlu kurumlar ve karakterlerin kişisel geçmişleri henüz ayrıntılı olarak kararlaştırılmamıştır.

---

## 4. Oyuncunun amacı

Oyuncunun maçtaki temel amacı **hayatta kalan son operatör veya takım olmak** ve AURA bölgesinden çıkış hakkını kazanmaktır.

Oyuncu bunu gerçekleştirmek için:

- Haritaya iniş yapar ve güvenli bir başlangıç noktası seçer.
- Silah, mühimmat ve ekipman toplar.
- Rakip operatörlerle çatışır veya çatışmadan kaçınarak pozisyon alır.
- Karakterinin özel yeteneğini doğru zamanda kullanır.
- Daralan güvenli alana uyum sağlar.
- Final çatışmasında hayatta kalarak tahliye hakkını kazanır.

### Maçın kazanma koşulu

- **Solo:** Maçın sonunda hayatta kalan son oyuncu kazanır.
- **Duo:** Hayatta kalan son ikili takım kazanır.
- **Squad:** Hayatta kalan son takım kazanır.

AURA verileri ve araştırma kayıtları, hikâyeyi ve harita keşfini destekleyen hedefler olarak kullanılabilir. Bunların maçın zorunlu kazanma koşulu olup olmayacağı henüz kesinleşmemiştir. İlk prototipte ana kazanma koşulu son hayatta kalan oyuncu/takım olacaktır.

---

## 5. Maç akışı

Bir maçın önerilen temel akışı şöyledir:

1. **Operatör seçimi:** Oyuncu maç öncesinde karakterini seçer.
2. **Maç başlangıcı ve iniş:** Oyuncu haritaya girer ve başlangıç bölgesini belirler.
3. **Keşif ve loot:** Oyuncu binaları, depoları ve diğer alanları keşfederek silah ve ekipman toplar.
4. **İlk çatışmalar:** Oyuncular avantajlı ekipman ve pozisyonlar için mücadele eder.
5. **Güvenli alanın daralması:** AURA çevreleme sistemi aktif alanı küçültür ve oyuncuları birbirine yaklaştırır.
6. **Orta ve ileri aşama:** Kaynak yönetimi, doğru rota, takım koordinasyonu ve yetenek kullanımı önem kazanır.
7. **Final çatışması:** Kalan oyuncular veya takımlar son güvenli bölgede karşılaşır.
8. **Sonuç:** Hayatta kalan son oyuncu/takım kazanır.

### Önerilen maç ölçüleri

| Değer | Hedef / durum |
|---|---|
| Maç süresi | 10–15 dakika hedefi |
| Tam oyun oyuncu sayısı | 40–60 oyuncu hedefi; performans testine bağlı |
| İlk çok oyunculu prototip | Yaklaşık 20 oyuncu hedefi |
| Modlar | Solo / Duo / Squad |
| İlk harita prototipi | Yaklaşık 300 × 300 metre test alanı |

Bu değerler kesinleşmiş teknik sınırlar değil, ilk denemeler için tasarım hedefleridir. Gerçek maç temposu ve sunucu performansına göre değiştirilecektir.

---

## 6. Battle Royale kuralları

### Temel kurallar

- Oyuncular maç öncesinde operatör seçer.
- Silahlar ve temel ekipmanlar maç içinde bulunur.
- Oyuncular haritada ganimet ararken diğer oyuncularla karşılaşır.
- Güvenli alan zamanla küçülür.
- Güvenli alan dışında kalan oyuncular çevresel tehlikeden etkilenir.
- Elenen oyuncu mevcut maçtan çıkar.
- Son hayatta kalan oyuncu veya takım kazanır.

### Yeniden doğma

Klasik Battle Royale yapısını korumak için ilk tasarımda maç içinde otomatik yeniden doğma bulunmaz. Duo ve Squad modlarında takım arkadaşını geri getirme sistemi düşünülürse, bu ayrı bir mekanik olarak tasarlanıp dengelenmelidir.

### Oyun modları

- **Solo:** Her oyuncu kendi adına mücadele eder.
- **Duo:** İki kişilik takımlar.
- **Squad:** Daha büyük takım formatı; takım büyüklüğü daha sonra belirlenecek.

---

## 7. Operatör sistemi

Oyuncu maçtan önce bir operatör seçer. Her operatörün:

- Bir aktif yeteneği,
- Bir pasif yeteneği,
- Kendine özgü görsel silüeti,
- Renk ve ekipman kimliği,
- Belirli bir oyun tarzı bulunur.

Operatörler silah sınıfına kilitlenmez. Her karakter maçta bulduğu uygun silahları kullanabilir. Böylece karakter seçimi oyun tarzını belirlerken maç içindeki ekipman seçimi değişken kalır.

### Yetenek tasarım ilkeleri

- Yetenekler rakibi anında ve kaçınılmaz biçimde yenmemeli.
- Yeteneklerin kullanım zamanı ve bekleme süresi önemli olmalı.
- Rakibin karşılık verebileceği veya önlem alabileceği durumlar bulunmalı.
- Bilgi veren yetenekler rakibin tam konumunu sürekli göstermemeli.
- Görsel ve işitsel efektler, yeteneğin kullanıldığını anlaşılır biçimde belirtmeli.

---

## 8. İlk operatör kadrosu

İlk kadro için dört çalışma karakteri belirlenmiştir. İsimler ve yetenekler tasarım prototipidir; denge testleriyle değiştirilebilir.

### 8.1 İZ — Keşif uzmanı

**Rol:** Keşif / bilgi toplama  
**Ana renkler:** Haki yeşil, kırık beyaz, koyu gri ve az miktarda turuncu.

**Görünüm:** Hafif taktik kıyafet, geniş vizörlü keşif başlığı, bilek tarayıcısı ve sırtında kompakt algılama cihazı. Silüeti hafif ve hareketli görünmelidir.

- **Aktif — İz Taraması:** Çevresine tarama dalgası gönderir ve yakın zamanda kullanılmış hareket izlerini kısa süreliğine görünür kılar. Rakibin anlık konumunu göstermez ve duvarların arkasını görmez.
- **Pasif — Arazi Okuyucu:** Yakınındaki taze hareket izlerini diğer operatörlerden biraz daha uzun süre fark edebilir.

**İlk deneme değerleri (geçici):**

| Parametre | Öneri |
|---|---:|
| Tarama yarıçapı | 18 metre |
| Algılanan izin yaşı | En fazla 5 saniye |
| İzlerin görünür kalması | 2,5 saniye |
| Aktif yetenek bekleme süresi | 35 saniye |

**Animasyonlar:** Idle, Walk, Run, Aim, Fire, Reload, Scan, Loot.

### 8.2 KİLİT — Savunma uzmanı

**Rol:** Savunma / alan tutma  
**Ana renkler:** Turuncu ve koyu gri.

**Görünüm:** Daha kalın koruma plakaları, mekanik eldivenler ve sırtında katlanabilir bariyer modülü.

- **Aktif — Enerji Bariyeri:** Kısa süreli ve belirli bir yönde koruma sağlayan bariyer oluşturur.
- **Pasif — Hazırlıklı Ekipman:** Savunma ekipmanlarından daha verimli yararlanır. Kesin etkisi denge testinde belirlenecek.

### 8.3 AKS — Hareket uzmanı

**Rol:** Hareketlilik / pozisyon değiştirme  
**Ana renkler:** Mavi ve beyaz.

**Görünüm:** Hafif kıyafet, hareket destekli botlar ve bel kısmında deneysel enerji modülü.

- **Aktif — Faz Geçişi:** Kısa mesafeli ve sınırlı bir hızlı yer değiştirme gerçekleştirir. Mesafe, engel etkileşimi ve bekleme süresi test edilmelidir.
- **Pasif — Hareket Desteği:** Hareket kabiliyetine küçük bir avantaj sağlar. Kesin etkisi daha sonra belirlenecek.

### 8.4 EKO — Aldatma uzmanı

**Rol:** Aldatma / dikkat dağıtma  
**Ana renkler:** Mor ve camgöbeği.

**Görünüm:** Hafif taktik kıyafet, sırtında holografik projektörler ve omuzlarında küçük görüntü oluşturma cihazları.

- **Aktif — Holografik Kopya:** Rakiplerin dikkatini dağıtmak için kısa süreli sahte operatör görüntüsü üretir.
- **Pasif — Düşük İz:** Algılama sistemleri tarafından daha zor fark edilmesini sağlayan küçük bir avantaj sunar. Kesin etkisi denge testinde belirlenecek.

### Karakter görsel standardı

- Karakterler uzaktan kolayca ayırt edilmelidir.
- Her operatörün silüeti ve ana renkleri farklı olmalıdır.
- Kafa, el ve ayak oranları hafif stilize edilebilir.
- Kıyafet ve ekipmanlar aynı evrene ait görünmelidir.
- Sırt çantası, başlık, eldiven ve benzeri parçalar mümkün olduğunca modüler tasarlanmalıdır.

---

## 9. Silah ve ekipman sistemi

Silahlar maç sırasında bulunur. Operatör seçimi oyuncunun kullanabileceği silahları kısıtlamaz.

### İlk silah kategorileri

| Kategori | Genel kullanım |
|---|---|
| Saldırı tüfeği | Orta mesafede dengeli kullanım |
| Pompalı tüfek | Yakın mesafede yüksek etki |
| Hafif makineli | Yakın mesafede hareketli çatışma |
| Keskin nişancı tüfeği | Uzun mesafede hassas atış |
| Hafif makineli tüfek | Daha uzun süreli ateş ve alan baskısı |

### Eklenti fikirleri

İlerleyen aşamalarda silahlara şu eklentiler eklenebilir:

- Dürbün ve nişangâhlar,
- Şarjör seçenekleri,
- Namlu eklentileri,
- Geri tepme veya kontrol geliştirmeleri.

İlk prototipte eklenti sistemi zorunlu değildir. Önce temel silah toplama, ateş etme, yeniden doldurma ve mühimmat yönetimi tamamlanmalıdır.

---

## 10. Ganimet (loot) sistemi

Ganimetler binalarda, sandıklarda ve belirlenmiş harita noktalarında bulunur. İçerik her maçta değişebilir.

### Sandık türleri

| Sandık | Görsel kimlik | İçerik hedefi |
|---|---|---|
| Normal sandık | Ahşap / yeşil | Temel silah ve ekipman |
| Teknoloji sandığı | Mavi | Daha gelişmiş ekipman |
| Özel kasa | Turuncu | Nadir ekipman veya yüksek değerli ganimet |

### Ganimet dağıtımı

- Her bölgenin ganimet yoğunluğu ve risk seviyesi farklı olabilir.
- Aynı sandık her maçta aynı eşyaları vermemelidir.
- Yüksek riskli bölgeler daha değerli ganimet içerebilir.
- Oyuncunun envanter kapasitesi ve eşya yönetimi daha sonra belirlenecektir.

### İlk prototipte gerekenler

- Yerde bulunan silah,
- Mühimmat,
- Bir temel iyileştirme veya koruma eşyası,
- Açılabilir sandık,
- Eşya toplama ve envantere ekleme.

---

## 11. Güvenli alan ve AURA sistemi

Klasik zehirli gaz veya sıradan fırtına yerine, güvenli alanın daralmasını AURA tesisinin **çevreleme sistemi** ile açıklamak hedeflenmektedir.

AURA'nın çevresel kontrol ağı, adanın bazı bölgelerini erişime kapatır. Güvenli alan dışında kalan operatörler sistemin tehlikeli etkisine maruz kalır ve güvenli bölgeye dönmek zorunda kalır.

### Tasarım hedefleri

- Güvenli alan aşamalı biçimde küçülür.
- Oyuncuların rotalarını ve zamanlamalarını planlaması gerekir.
- Alan dışındaki tehlike görsel ve işitsel olarak anlaşılır olmalıdır.
- Final bölgesi oyuncuları çatışmaya yönlendirmelidir.
- AURA çevreleme sistemi oyunun yakın gelecek atmosferiyle uyumlu olmalıdır.

Daralma süreleri, hasar değerleri ve alan geometrisi henüz belirlenmemiştir. Bunlar maç testleriyle ayarlanacaktır.

---

## 12. İlk harita: AURA-01

**Harita adı:** AURA-01: Saha İstasyonu  
**Harita türü:** Araştırma adası  
**İlk prototip alanı:** Yaklaşık 300 × 300 metre (öneri)

AURA-01, araştırma tesisleri, küçük yerleşim alanları, ormanlık bölgeler ve enerji altyapısından oluşan bir adadır. Amaç, her bölgenin farklı çatışma mesafesi, görüş imkânı ve ganimet riski sunmasıdır.

### 12.1 AURA Merkezi

- Büyük araştırma kompleksi.
- Laboratuvarlar ve kontrol odaları.
- Açık avlu ve bağlantılı koridorlar.
- Yüksek risk ve yüksek ganimet hedefi.
- Haritanın ana ilgi noktası.

### 12.2 Servis Alanı

- Bakım atölyeleri.
- Konteynerler ve küçük depolar.
- Dar geçitler ve siper alınabilecek objeler.
- Yakın mesafe çatışmalarına uygun yapı.

### 12.3 Yerleşim

- Evler ve küçük işletmeler.
- Sokaklar, bahçeler ve iç mekânlar.
- Orta seviyede ganimet.
- Bina içi ve dışı çatışma seçenekleri.

### 12.4 Çamlık

- Ağaçlık alanlar ve kayalık yollar.
- Küçük gözlem noktaları.
- Daha sınırlı görüş mesafesi.
- Gizlenme ve rota değiştirme imkânı.

### 12.5 Enerji Bahçesi

- Güneş panelleri ve enerji istasyonları.
- Daha açık görüş hatları.
- Uzun mesafe çatışmaları için uygun alanlar.
- Orta seviyede ganimet.

### Önerilen yerleşim şeması

```text
                         KUZEY
        +----------------+----------------+
        |                |                |
        |    ÇAMLIK      |  GÖZLEM        |
        |                |  NOKTASI       |
        +----------------+----------------+
        |                |                |
        | SERVİS ALANI   |  AURA MERKEZİ  |
        |                |                |
        +----------------+----------------+
        |                |                |
        | YERLEŞİM       | ENERJİ BAHÇESİ |
        |                |                |
        +----------------+----------------+
                         GÜNEY
```

Bu şema yalnızca ilk yerleşim fikridir; ölçekli veya nihai harita değildir. Yollar, yükseklikler, binalar, iniş noktaları ve güvenli alan geçişleri blockout testleriyle düzenlenecektir.

### Harita için ilk çevre varlıkları

| Varlık | İlk hedef adet |
|---|---:|
| Laboratuvar binası | 1 |
| Ev modeli | 1 |
| Depo modeli | 1 |
| Ağaç modeli | 3 |
| Kaya modeli | 2 |
| Sandık modeli | 2 |
| Konteyner modeli | 2 |
| Silah modeli | 1 |
| Keşif tarayıcısı | 1 |

Bu liste prototip içindir. Varlıklar tekrar kullanılabilir modüler parçalar olarak tasarlanmalıdır.

---

## 13. Sanat yönetimi

### Genel görsel yaklaşım

LOST ISLAND'ın görsel stili **Dengeli Stylized 3D** olarak belirlenmiştir.

Bu yaklaşım:

- Aşırı çizgi film görünümünden kaçınır.
- Tam gerçekçi askerî simülasyon estetiğini hedeflemez.
- Stilize karakterleri doğal görünümlü çevrelerle birleştirir.
- Modern araştırma tesisleri ve teknolojik ekipmanları temiz, anlaşılır şekillerle gösterir.
- Uzaktan okunabilir silüetleri ve kontrollü renk kullanımını önemser.

### Renk paleti

| Kullanım | Renk | HEX |
|---|---|---|
| Doğa | Yeşil | `#4C9662` |
| Toprak | Sıcak sarı | `#E5B76A` |
| Teknoloji | Mavi | `#58A9D6` |
| Uyarı | Turuncu | `#E77C48` |
| İZ | Haki | `#4F8065` |
| İZ | Kırık beyaz | `#E8E4D6` |
| İZ | Koyu gri | `#303B42` |
| İZ | Turuncu detay | `#F2A44A` |

### Model ve materyal ilkeleri

- Orta veya düşük poligonlu, temiz formlu modeller tercih edilir.
- Silüet ve büyük şekiller küçük yüzey detaylarından daha önemlidir.
- Materyaller stilize PBR yaklaşımında olmalıdır.
- Yüzeylerde aşırı parlaklık ve görsel gürültü kullanılmamalıdır.
- Çevre, karakteri ve ganimeti görünmez hâle getirmemelidir.
- Aynı malzeme ve renk dili bütün haritada tutarlı olmalıdır.

### Karakter ve çevre dengesi

Karakterler daha belirgin renk ve şekillere sahip olabilir. Çevre ise atmosferi oluştururken oyuncunun dikkatini gereksiz yere dağıtmamalıdır. Rakip oyuncular, ganimet ve önemli etkileşim nesneleri her zaman kolay seçilmelidir.

---

## 14. Kamera ve okunabilirlik

### Kamera önerisi

- **Projection:** Orthographic.
- **Açı:** Yaklaşık 40–45 derece eğim başlangıç noktası.
- **Dönüş:** İzometrik görünüm için yaklaşık 45 derece başlangıç yönü; oyun testine göre ayarlanabilir.
- **Takip:** Oyuncuyu yumuşak biçimde takip eden kamera.
- **Sınırlar:** Kamera harita dışına çıkmamalı.

### Çevre gizleme

Oyuncu bir binanın içine girdiğinde çatının veya görüşü engelleyen parçaların şeffaflaşması ya da geçici gizlenmesi gerekir. Ağaç ve büyük objeler de karakterin konumunu tamamen kapatmamalıdır.

### Rekabetçi okunabilirlik

- Oyuncu ve düşman silüeti seçilebilir olmalı.
- Mermi ve namlu efektleri anlaşılır olmalı.
- Yetenek efektleri renk ve şekil bakımından ayırt edilmeli.
- Ganimetlerin nadirlik göstergeleri kolay okunmalı.
- Ekran efektleri görüşü gereksiz yere kapatmamalı.

---

## 15. Animasyon, ses ve görsel efektler

### İlk animasyon paketi

| Animasyon | Öncelik |
|---|---|
| Idle / Bekleme | Yüksek |
| Walk / Yürüme | Yüksek |
| Run / Koşma | Yüksek |
| Aim / Nişan alma | Yüksek |
| Fire / Ateş etme | Yüksek |
| Reload / Şarjör değiştirme | Orta |
| Scan / Tarama yeteneği | Yüksek |
| Loot / Eşya toplama | Orta |

### Görsel efekt dili

- **Kalkan:** Mavi-beyaz enerji yüzeyi ve belirgin etkinleşme efekti.
- **Hologram:** Mor/camgöbeği tonlarında yarı saydam görüntü ve hafif dijital bozulma.
- **Silah:** Net ama ekranı kaplamayan namlu parlaması ve mermi izi.
- **Loot:** Eşyanın değerini gösteren kontrollü renk ve ışık efekti.
- **İz taraması:** Zemine yayılan, kısa süreli mavi tarama dalgası.

Efektler stilize olmalı, fakat çatışmanın okunmasını zorlaştırmamalıdır.

Ses tasarımında silahlar, adım sesleri, yetenek etkinleşmeleri, ganimet açılması ve güvenli alan uyarıları birbirinden ayırt edilebilir olmalıdır. Ayrıntılı ses listesi sonraki üretim aşamasında hazırlanacaktır.

---

## 16. Arayüz ve kullanıcı deneyimi

İlk arayüz tasarımında öncelik, maç sırasında gerekli bilgileri oyuncuya hızlı, temiz ve anlaşılır biçimde sunmaktır.

### Reaktif ve Olay Güdümlü HUD İlkesi (Sıfır Polling)
Arayüz bileşenleri `Update()` döngüsü içerisinde her karede veri çekmez (polling yapılmaz). Can, mühimmat, yetenek süresi veya bölge bilgisi değiştiğinde fırlatılan C# ve ScriptableObject olaylarına abone olunur; UI yalnızca veri değiştiğinde kendini günceller. Bu, gereksiz işlemci yükünü sıfırlarken kusursuz bir tepki süresi sağlar.

### Oyun içi HUD için düşünülen öğeler
- **Sağlık ve Zırh Barı:** `OnHealthChanged` ve `OnShieldChanged` eventlerine reaktif bağlı.
- **Mevcut Silah ve Mühimmat Sayacı:** `OnAmmoChanged` ve `OnWeaponSwapped` eventleri ile güncellenir.
- **Aktif Yetenek Göstergesi:** `OnAbilityCast` ve `OnAbilityCooldownChanged` ile dinamik dolum çemberi.
- **Envanter ve Ekipman Tablosu:** `OnInventoryChanged` eventiyle tetiklenen hızlı görsel yuvalar.
- **Güvenli Alan Bilgilendirmesi:** `OnZonePhaseChanged` ile kalan süre, aşama ve rota uyarısı.
- **Kalan Oyuncu / Takım Sayacı:** `OnPlayerCountChanged` eventiyle canlı güncellenen rekabet paneli.
- **Yüzen Hasar ve Vuruş Geri Bildirimi:** `OnHitConfirmed` ve `OnDamageTaken` ile nesne havuzundan (ObjectPool) çağrılan hasar sayıları (Damage Popup).
- **Mini Harita:** Çevreleyen alan sınırını ve takım arkadaşlarını gösteren dinamik radar.

### Menü ekranları
- Ana menü.
- Operatör seçimi.
- Oyun modu ve eşleştirme.
- Ayarlar.
- Maç sonucu.
- Koleksiyon ve kozmetik ekranları (ileriki aşama).

**Platform notu:** Oyunun kesin hedef platformu henüz kararlaştırılmamıştır. Bu nedenle HUD ve kontrol sistemi hem dokunmatik hem de klavye/fare düzenine uyarlanabilecek şekilde tasarlanmalıdır; platform kararı verildiğinde giriş ve ekran ölçekleme standartları netleştirilmelidir.

---

## 17. Teknik yön ve Unity yapısı

LOST ISLAND, endüstri standardı **profesyonel yöntemler**, **sunucu otoriteli (server-authoritative)** yapı ve **olay güdümlü mimari (Event-Driven Architecture)** prensipleriyle geliştirilmektedir.

### Şu anki teknik tercihler

| Alan | Tercih | Standart |
|---|---|---|
| Motor | Unity 6 | LTS stabilite, URP desteği |
| Render Pipeline | URP | Universal Render Pipeline, optimize mobil/PC performansı |
| Kamera | Orthographic izometrik | Temiz görüş açısı ve rekabetçi okunabilirlik |
| Ağ Mimarisi | Mirror + KCP | Düşük gecikmeli UDP, Dedicated Server otoritesi |
| Yazılım Mimarisi | Event-Driven & Decoupled | ScriptableObject Game Events, C# Action/Events, Observer Pattern |
| Bellek & Performans | Object Pooling | Mermiler, VFX, SFX ve metinler için sıfır GC tahsisi |
| Kod Modülerliği | Assembly Definitions (asmdef) | Katmanlı mimari, sirküler bağımlılıkları engelleme |
| Giriş Sistemi | Unity New Input System | C# Event tabanlı, çoklu girdi aygıtı desteği |
| Model stili | Stylized, düşük/orta poligon | Yüksek kare hızı ve silüet netliği |
| Materyal | Stilize PBR | Dengeli ışıklandırma ve görsel hiyerarşi |
| Efektler | Particle System ve Shader Graph | Performanslı görsel efektler |
| Animasyon | Humanoid Rig | Modüler operatör animasyon hattı |

### Profesyonel Kodlama & Mimari Standartları

1. **Olay Güdümlü Sistemler (Event-Driven Architecture):**
   - Sınıflar birbirine doğrudan sıkı referanslar bağlamaz (Loose Coupling).
   - Oynanış mantığı (Combat, Health, Zone), UI veya Ses sistemlerini doğrudan çağırmaz. Sistemler event yayınlar; UI, VFX ve SFX dinleyici olarak reaksiyon verir.
2. **Sıfır Polling Prensibi (Push-based Architecture):**
   - `Update()` içinde değişken yoklama (polling) kesinlikle yasaktır.
3. **ScriptableObject Odaklı Tasarım (Data-Driven):**
   - Tüm silah, zırh, operatör ve yetenek dengesi ScriptableObject varlıkları üzerinden yönetilir; kodun içine sabit sayı yazılmaz.
4. **Merkezi Nesne Havuzlama (Object Pooling):**
   - Çatışma anında kare atlamalarını (GC lag spikes) önlemek amacıyla mermiler, efektler ve ses kaynakları havuzlanır; `Instantiate` ve `Destroy` çalışma zamanında çağrılmaz.
5. **Modüler Assembly Definition Yapısı:**
   - Proje katmanları izole asmdef dosyaları ile ayrılır (`Core`, `Gameplay`, `Networking`, `UI`).

### Önerilen başlangıç klasör yapısı

```text
Assets/
└── _Project/
    ├── Assemblies/             # LostIsland.Core, Gameplay, Network, UI asmdef'leri
    ├── Art/
    │   ├── Characters/
    │   ├── Environment/
    │   ├── Weapons/
    │   ├── Props/
    │   └── Materials/
    ├── Audio/
    │   ├── Music/
    │   └── SFX/
    ├── Data/                   # ScriptableObject Tanımları ve SO Game Events
    │   ├── Operators/
    │   ├── Weapons/
    │   ├── Zone/
    │   └── Events/
    ├── Prefabs/
    │   ├── Characters/
    │   ├── Weapons/
    │   ├── Loot/
    │   ├── VFX/
    │   └── UI/
    ├── Scenes/
    │   ├── Bootstrap/
    │   ├── MainMenu/
    │   └── Prototype_AURA01/
    └── Scripts/
        ├── Core/               # EventBus, ObjectPool, Interfaces (IDamageable, IPoolable)
        ├── Networking/         # Mirror senkronizasyonu ve sunucu RPC yöneticileri
        ├── Player/             # Hareket, izometrik kamera ve girdi dinleyicileri
        ├── Combat/             # Hasar alma, can, zırh, vuruş doğrulaması
        ├── Weapons/            # Silah atış mantığı ve şarjör yönetimi
        ├── Operators/          # Aktif ve pasif operatör yetenek sınıfları
        ├── Loot/               # Sandık açma, yer eşyası ve envanter
        ├── Zone/               # AURA çevreleme alanı ve alan dışı hasar
        └── UI/                 # Reaktif HUD ve menü denetleyicileri
```

### Önce geliştirilecek sistemler

1. **Mimari Çekirdek:** EventBus, ScriptableObject GameEvent altyapısı ve ObjectPool sistemi.
2. **Kamera ve Hareket:** İzometrik kamera ve New Input System tabanlı sunucu onaylı karakter kontrolü.
3. **Event-Driven Çatışma:** Hitscan silah atışı, `IDamageable`, `OnHealthChanged` ve `OnHitConfirmed` olayları.
4. **Reaktif Arayüz:** Sıfır polling ile çalışan dinamik can barı ve mühimmat sayacı.
5. **Loot ve Envanter:** Sandık açma, silah toplama ve mühimmat eşitleme.
6. **Operatör Sistemi:** İZ operatörünün aktif (İz Taraması) ve pasif yeteneği.
7. **AURA Güvenli Alanı:** Zaman ayarlı daralan çember ve alan hasarı eventleri.
8. **Ağ Doğrulaması:** Dedicated Server üzerinde hareket, vuruş ve maç döngüsü kontrolü.
9. **Maç Durum Makinesi:** Lobi, geri sayım, aktif çatışma, eleme ve sonuç ekranı.

---

## 18. İlk prototip kapsamı

İlk hedef tam bir Battle Royale üretmek değil, oyunun temel hissini doğrulayan küçük bir **vertical slice** hazırlamaktır.

### İlk oynanabilir prototipte bulunacaklar

- AURA-01'in küçük bir blockout sürümü.
- Tek oynanabilir karakter: İZ.
- İzometrik kamera.
- Yürüme, koşma ve nişan alma.
- Tek temel silah.
- Ateş etme, mermi ve yeniden doldurma.
- Birkaç loot nesnesi ve sandık.
- Basit sağlık/hasar sistemi.
- İZ'in tarama yeteneği.
- Basit güvenli alan daralması.
- Tek oyunculu test akışı.

### İlk prototipte yapılmayacaklar

- Bütün operatörlerin tamamlanmış modelleri.
- Büyük ve ayrıntılı tam ada haritası.
- Kozmetik mağazası ve ilerleme sistemi.
- Sezon sistemi.
- Gelişmiş eşleştirme ve sosyal sistemler.
- Nihai oyuncu sayısı için ölçeklenmiş sunucu altyapısı.

Önce küçük sahnede hareket, kamera, çatışma ve loot döngüsünün eğlenceli olup olmadığı test edilmelidir.

---

## 19. Üretim yol haritası

### Aşama 1 — Oynanış ve Olay Güdümlü Temel (Core & Architecture)

- EventBus, ScriptableObject GameEvent ve ObjectPool çekirdek mimarisinin kurulması.
- Unity New Input System ile event bazlı girdi yayını.
- İzometrik kamera kurulumu ve karakter hareketi.
- İlk silah mekaniği (hitscan atış ve object pooling ile mermiler).
- Event-driven sağlık, can barı ve hasar sistemi (`OnDamageTaken`, `OnHealthChanged`).
- Reaktif HUD (Sıfır polling ile çalışan dinamik arayüz).

**Çıkış ölçütü:** Oyuncu sahnede hareket edip hedefe ateş edebilmeli; can ve mühimmat event-driven olarak HUD'a anlık yansımalı.

### Aşama 2 — Loot ve Operatör Sistemi

- Silah toplama, envanter ve mühimmat eventleri (`OnItemPickedUp`, `OnAmmoChanged`).
- Sandık açma mekaniği.
- İZ'in aktif (İz Taraması) ve pasif yeteneği.
- Yetenek HUD göstergesi ve bekleme süresi eventleri.

**Çıkış ölçütü:** Oyuncu haritayı keşfedip ekipman toplayabilmeli, yeteneğini kullanabilmeli ve tüm bu akış loosely-coupled eventlerle çalışmalı.

### Aşama 3 — Harita ve Battle Royale Döngüsü

- AURA-01 blockout haritası.
- Farklı bölgeler ve bina girişleri (çatı gizleme/şeffaflaşma).
- Ganimet noktaları ve sandık dağılımı.
- Güvenli alan daralma mekaniği (`OnZonePhaseChanged`).
- Maç durum makinesi (State Machine: başlangıç, eleme ve sonuç ekranı).

**Çıkış ölçütü:** Baştan sona tek oyunculu olarak oynanabilen temel bir maç döngüsü oluşmalı.

### Aşama 4 — Çok Oyunculu Prototip (Dedicated Server)

- Mirror + KCP ağ entegrasyonu.
- Sunucu otoriteli hareket, çatışma ve hasar doğrulaması.
- Ağ olayları (RPC/Commands) ile istemci-sunucu event köprüsünün bağlanması.
- Oyuncu elenmesi ve maç sonucunun sunucu tarafından yönetilmesi.

**Çıkış ölçütü:** Birden fazla oyuncu aynı dedicated server'da adil ve güvenilir biçimde mücadele edebilmeli.

### Aşama 5 — Görsel Kalite, Ses ve İçerik

- İZ'in nihai modeli ve animasyonları.
- AURA-01 çevre modelleri, materyal ve URP ışıklandırması.
- Olaylara bağlı ses (SFX) ve görsel efektler (VFX) entegrasyonu.
- KİLİT, AKS ve EKO operatörleri.
- Diğer silah sınıfları (Shotgun, SMG, Sniper, LMG) ve ganimet sandıkları.

**Çıkış ölçütü:** Prototip, LOST ISLAND'ın hedeflenen görsel kimliğini ve atmosferini yansıtmalı.

### Aşama 6 — Denge, Optimizasyon ve Profilleme

- Silah ve operatör yetenek dengesi.
- Sıfır GC tahsisi optimizasyonları ve nesne havuzlama doğrulaması.
- Güvenli alan temposu ve ganimet dağılımı.
- FPS, bellek ve ağ paketi optimizasyonu (profiler testleri).

**Çıkış ölçütü:** Maçlar adil, anlaşılır, stabil ve yüksek performansla tekrar oynanabilir olmalı.

---

## 20. Henüz kararlaştırılmamış konular

Aşağıdaki konular fikir aşamasındadır ve test edilmeden kesin tasarım kararı sayılmamalıdır:

- Nihai oyuncu sayısı: 40–60 hedefi gerçek performans testine bağlıdır.
- Maç süresi: 10–15 dakika hedefi test edilmelidir.
- Kesin platform: PC, mobil veya çoklu platform kararı verilmelidir.
- Kontroller: Dokunmatik veya klavye/fare düzeni platform kararına göre hazırlanmalıdır.
- Squad takım büyüklüğü.
- Silahların hasar, menzil, atış hızı ve geri tepme değerleri.
- Zırh, iyileştirme ve envanter kuralları.
- Operatör yeteneklerinin kesin değerleri ve bekleme süreleri.
- Güvenli alanın aşama sayısı, süresi ve hasarı.
- AURA çevreleme sisteminin nihai görsel/işitsel sunumu.
- Backend eşleştirme (matchmaking) ve sunucu yönetim servisi mimarisi.
- AURA verisinin maç içindeki rolü: yalnızca hikâye/yan hedef mi, yoksa özel bir final amacı mı?
- Karakterlerin kişisel geçmişleri ve AURA olayının gerçek nedeni.

---

## 21. Terimler sözlüğü

| Terim | Açıklama |
|---|---|
| LOST ISLAND | Oyunun çalışma adı. |
| AURA | Adadaki deneysel teknoloji programı ve tesis ağı. |
| AURA-01 | İlk oynanabilir haritanın adı. |
| Operatör | Oyuncunun maç öncesinde seçtiği karakter. |
| Aktif yetenek | Oyuncunun uygun zamanda tetiklediği özel yetenek. |
| Pasif yetenek | Sürekli veya koşula bağlı çalışan karakter özelliği. |
| Loot | Haritada bulunan silah, mühimmat ve ekipmanlar. |
| Güvenli alan | Maç sırasında oyuncuların kalması gereken aktif bölge. |
| Çevreleme sistemi | Hikâye içinde güvenli alanın daralmasını açıklayan AURA sistemi. |
| Blockout | Haritanın nihai modellerden önce basit geometrilerle kurulmuş taslağı. |
| Vertical slice | Oyunun temel deneyimini baştan sona gösteren küçük ama oynanabilir bölüm. |
| URP | Unity Universal Render Pipeline. |

---

## Proje özeti

LOST ISLAND'ın özü; **özel yetenekli operatörler, maç içinde bulunan silahlar, keşif ve ganimet toplama, izometrik çatışma ve daralan AURA bölgesi** üzerine kurulu bir Battle Royale deneyimidir.

İlk hedef, büyük harita veya çok sayıda karakter üretmek değil; İZ karakteri, tek silah ve küçük AURA-01 blockout haritasıyla hareket–çatışma–loot döngüsünü eğlenceli hâle getirmektir.

> **LOST ISLAND — AURA Island. Hayatta kal. Veriyi bul. Tahliye hakkını kazan.**
