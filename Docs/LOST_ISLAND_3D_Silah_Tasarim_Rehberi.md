# LOST ISLAND

## 3D Silah Tasarım Rehberi

**Belge türü:** Sanat yönetimi ve modelleme standardı\
**Sürüm:** 1.0\
**Durum:** İlk görsel standart --- geliştirilebilir

------------------------------------------------------------------------

## 1. Belgenin amacı

Bu belge, LOST ISLAND projesinde üretilecek tüm silah modellerinin aynı
görsel dünyaya ait görünmesini sağlamak için hazırlanmıştır.

Hedef; gerçekçi silah kopyaları üretmek değil, oyunun stiline uygun,
kolay tanınan, temiz silüetli ve kendine özgü kimliği olan **stilize 3D
oyun varlıkları** oluşturmaktır.

Bu kurallar yeni bir silah tasarlanırken konsept aşamasından Unity
içindeki son görünüme kadar referans alınmalıdır.

## 2. LOST ISLAND sanat yönetimi

### 2.1 Ana stil

-   Stil: **Stylized Low-Poly / Chunky 3D**
-   Genel görünüm: Sade, güçlü, okunaklı ve oyunlaştırılmış.
-   Form dili: Geniş yüzeyler, belirgin hacimler ve kontrollü köşeler.
-   Detay seviyesi: Orta-düşük; uzaktan okunurluğu bozan mikro
    detaylardan kaçınılır.
-   Yüzey karakteri: Ağırlıklı olarak mat veya hafif saten.
-   Gerçekçilik seviyesi: Fotoğraf gerçekçiliği hedeflenmez.

### 2.2 Görsel öncelik sırası

Bir model değerlendirilirken aşağıdaki sıra korunmalıdır:

1.  İlk bakışta tanınabilir silüet
2.  Oyuncunun kamera mesafesinde okunabilir ana parçalar
3.  Silaha özel renk kimliği
4.  Temiz ve tutarlı geometri
5.  Yakından bakıldığında fark edilen ikincil detaylar

Bir detay silahın genel okunurluğunu azaltıyorsa kullanılmamalıdır.

## 3. Geometri ve form kuralları

### 3.1 Genel geometri

-   Modeller basitleştirilmiş, hacimli ve stilize formlardan
    oluşturulmalıdır.
-   Ana gövde tek bakışta okunabilen büyük kütleler halinde
    tasarlanmalıdır.
-   Keskin ve ince gerçekçi ayrıntılar yerine kontrollü, belirgin yüzey
    geçişleri tercih edilmelidir.
-   Gereksiz çıkıntılar, küçük vidalar, yoğun oluklar ve karmaşık yüzey
    desenleri kullanılmamalıdır.
-   Silahın ana formunu desteklemeyen parçalar modele eklenmemelidir.
-   Modelin farklı açılardan görünümü tutarlı olmalıdır; yalnızca yan
    görünüşü iyi olan bir tasarım yeterli değildir.

### 3.2 Silüet

-   Her silahın uzaktan ayırt edilebilen kendine özgü bir dış hattı
    olmalıdır.
-   Ana kütleler birbirine karışmamalıdır.
-   İnce parçalar, oyun kamerasında kaybolmayacak ölçüde
    sadeleştirilmelidir.
-   Sınıfı aynı olan silahlar bile gövde oranı, genel hacim ve görsel
    vurgu bakımından birbirinden ayrılmalıdır.

### 3.3 Abartı ve oran

-   Gerçek dünyadaki ölçüler birebir kopyalanmak zorunda değildir.
-   Oyunda okunurluğu artırmak için bazı hacimler kontrollü biçimde
    büyütülebilir.
-   Abartı, modelin oyuncak gibi rastgele görünmesine değil, tutarlı bir
    oyun estetiğine hizmet etmelidir.
-   Bir modelde kullanılan oran dili, diğer silahlarla görsel olarak
    uyumlu olmalıdır.

## 4. Renk sistemi ve desen kimliği

### 4.1 Temel renk yaklaşımı

Silahların genelinde dengeli bir renk hiyerarşisi kullanılmalıdır:

-   **Ana renk:** Silahın kimliğini belirleyen geniş yüzeyler.
-   **İkincil renk:** Ana rengi destekleyen gövde veya parça yüzeyleri.
-   **Koyu nötr:** Birleştirici ve görsel denge sağlayan alanlar.
-   **Vurgu rengi:** Az miktarda kullanılan, silahı diğerlerinden ayıran
    renk.

Renkler birbirleriyle yarışmamalı; vurgu rengi modelin tamamını
kaplamamalıdır.

### 4.2 Başlangıç paleti

  -----------------------------------------------------------------------
  Renk rolü               Örnek renk              Kullanım
  ----------------------- ----------------------- -----------------------
  Koyu nötr               Antrasit / morumsu koyu Ortak koyu parçalar
                          gri                     

  Sıcak ana renk          Kum beji / turuncuya    İlk tüfek ailesi için
                          yakın sarı              

  Kırmızı vurgu           Kırmızı / mercan        Pompalı konsepti için

  Zeytin yeşili           Mat zeytin              Alternatif gövde
                                                  kimliği

  Mavi                    Orta koyulukta mavi     Özel varyant veya
                                                  farklı sınıf

  Mor                     Orta koyulukta mor      Özel varyant veya
                                                  farklı sınıf
  -----------------------------------------------------------------------

Bu renkler başlangıç referansıdır; her silahın bütün renkleri kullanması
gerekmez.

### 4.3 Silaha özel desen

-   Her silahın bir ana renk kimliği bulunmalıdır.
-   Desenler büyük ve sade geometrik alanlar halinde tasarlanmalıdır.
-   Şerit, panel veya renk blokları modelin formunu takip etmelidir.
-   Karmaşık kamuflaj, yoğun çizik ve rastgele leke dokuları temel stil
    olarak kullanılmamalıdır.
-   Desen, silahın silüetini ve ana parçalarını örtmemelidir.
-   Aynı sınıftaki varyantlarda ortak bir görsel dil korunabilir; ancak
    renk dağılımı veya vurgu biçimi farklılaştırılmalıdır.

## 5. Materyal ve yüzey standardı

### 5.1 Materyal karakteri

-   Ana yüzeyler mat veya hafif saten olmalıdır.
-   Plastik benzeri parçalar kontrollü, yumuşak yansımalar taşıyabilir.
-   Metal hissi gerektiğinde sınırlı parlaklık kullanılmalıdır.
-   Aşırı parlak, krom benzeri yüzeyler temel görünüm olarak tercih
    edilmemelidir.
-   Materyaller arasındaki fark, yüksek çözünürlüklü dokulardan çok renk
    ve yüzey değerleriyle anlatılmalıdır.

### 5.2 Doku kullanımı

-   Doku detayları düşük yoğunlukta tutulmalıdır.
-   Büyük renk blokları ve temiz yüzeyler önceliklidir.
-   Gürültülü yüzeyler, yoğun kir, pas ve gerçekçi aşınma efektleri bu
    sanat stilinin varsayılan parçası değildir.
-   Küçük detaylar yalnızca yakın planda gerçekten fark yaratıyorsa
    eklenmelidir.

## 6. Mevcut konsept referansları

### 6.1 İlk tüfek konsepti --- SCAR esintili

**Görsel kimlik** - Sıcak kum beji / turuncu ana gövde - Koyu antrasit
yardımcı parçalar - Turuncu geometrik vurgu - Kalın ve sade gövde
kütleleri - Temiz, stilize ve okunaklı dış hat

**Korunacak tasarım ilkeleri** - Ana gövde ile koyu parçalar arasında
net renk ayrımı - Az sayıda, büyük ve anlamlı yüzey detayı - Oyunun
genel low-poly form diliyle uyum

Bu konsept, gelecekteki tüfekler için oran ve görsel kalite referansı
olarak kullanılabilir; birebir kopyalanması zorunlu değildir.

### 6.2 Pompalı konsepti

**Görsel kimlik** - Koyu antrasit ana gövde - Kırmızı / mercan renkli
belirgin vurgu alanları - Büyük, sade ve hacimli formlar - SCAR
konseptiyle aynı geometri dili - Farklı sınıfı uzaktan belli eden özgün
silüet

**Korunacak tasarım ilkeleri** - Kırmızı rengin kontrollü ve seçici
kullanımı - Koyu ana gövde ile vurgu yüzeyleri arasında net ayrım -
Gereksiz küçük ayrıntılardan kaçınma

## 7. Silah aileleri arasında tutarlılık

Her silahın kendine ait kimliği olmalı; ancak bütün modeller aynı oyunun
sanat yönetiminden çıkmış gibi görünmelidir.

Ortak kalması gerekenler: - Geometrik sadelik - Hacimli ve stilize form
dili - Mat / saten yüzey yaklaşımı - Kontrollü renk kullanımı - Benzer
görsel detay yoğunluğu - Aynı kamera mesafesinde okunabilirlik

Farklılaşabilecek alanlar: - Ana renk ve vurgu rengi - Genel silüet -
Gövde oranları - Büyük yüzeylerin yerleşimi - Sınıfa özgü görsel
karakter

## 8. Modelleme ve teknik teslim standardı

Bu bölüm, modelin oyun varlığı olarak kullanılabilmesi için genel üretim
kurallarını tanımlar. Kesin poligon veya doku bütçeleri, hedef cihaz ve
performans testleri belirlendikten sonra ayrıca kararlaştırılmalıdır.

### 8.1 Model organizasyonu

-   Modelin ana parçaları düzenli isimlendirilmelidir.
-   Gereksiz iç yüzeyler ve görünmeyen geometri temizlenmelidir.
-   Pivot noktası, Unity'de kullanılacağı konuma göre tutarlı
    belirlenmelidir.
-   Ölçek ve yön bilgisi dışa aktarımdan önce kontrol edilmelidir.
-   Modelin orijinal çalışma dosyası saklanmalıdır.

### 8.2 Unity kontrolü

Model yalnızca modelleme yazılımında değil, Unity içindeki gerçek oyun
kamerasında da kontrol edilmelidir.

Kontrol edilecekler: - Oyuncunun elinde doğru ölçekte görünmesi - Kamera
uzaklığında ana silüetin kaybolmaması - Materyal renklerinin oyun
ışığında bozulmaması - Gereksiz parlama veya çok koyu alan oluşmaması -
Farklı çevre ışıklarında okunabilir kalması - Performansın hedef
platform için uygun olması

### 8.3 Teknik değerler

Poligon sayısı, texture çözünürlüğü, materyal sayısı ve shader seçimi
her varlığın görsel ihtiyacına ve hedef platforma göre belirlenmelidir.
Bu belge henüz kesin sayısal bütçe tanımlamaz.

## 9. Üretim iş akışı

Her yeni silah aşağıdaki sırayla hazırlanmalıdır:

1.  **Rol ve kimlik:** Silahın oyun içindeki sınıfı ve görsel karakteri
    belirlenir.
2.  **Silüet taslağı:** Önce genel dış hat ve büyük hacimler çözülür.
3.  **Renk taslağı:** Ana, ikincil, koyu nötr ve vurgu renkleri seçilir.
4.  **3D bloklama:** Büyük formlar oluşturulur; küçük detaylara
    geçilmez.
5.  **Stil kontrolü:** Model mevcut LOST ISLAND referanslarıyla
    karşılaştırılır.
6.  **Materyal:** Mat / saten yüzey yaklaşımı uygulanır.
7.  **Oyun içi test:** Model Unity'de gerçek kamera ve ışık altında
    incelenir.
8.  **Revizyon:** Okunurluğu azaltan veya sanat yönetiminden sapan
    unsurlar düzeltilir.
9.  **Teslim:** Model, materyaller ve kaynak dosyaları düzenli şekilde
    saklanır.

## 10. Kalite kontrol listesi

Her silah tamamlanmadan önce aşağıdaki maddeler kontrol edilmelidir.

### Görsel kalite

-   [ ] Silahın silüeti uzaktan kolayca ayırt ediliyor.
-   [ ] Ana ve ikincil renkler dengeli.
-   [ ] Vurgu rengi kontrollü kullanılmış.
-   [ ] Geometri stilize, hacimli ve tutarlı.
-   [ ] Gereksiz küçük detay bulunmuyor.
-   [ ] Materyaller aşırı parlak değil.
-   [ ] Model diğer LOST ISLAND silahlarıyla aynı dünyaya ait görünüyor.

### Oyun içi kalite

-   [ ] Unity'de doğru ölçekte görünüyor.
-   [ ] Oyun kamerasında okunaklı.
-   [ ] Işık altında renk kimliğini koruyor.
-   [ ] Model ve materyaller düzgün içe aktarılmış.
-   [ ] Hedef platform için performans kontrolü yapılmış.

## 11. Tasarımda kaçınılacaklar

-   Fotoğraf gerçekçiliğine yönelmek
-   Aşırı küçük mekanik detaylarla modeli doldurmak
-   Her silaha aynı renk dağılımını vermek
-   Çok fazla vurgu rengi kullanmak
-   Rastgele çizik, pas ve kir eklemek
-   Silüeti bozan gereksiz çıkıntılar oluşturmak
-   Yalnızca yakın plan görünümüne göre modellemek
-   Farklı silahların geometri ve materyal kalitesini birbirinden
    koparmak

## 12. Nihai tasarım ilkesi

**LOST ISLAND silahları; sade, hacimli, stilize, renk kimliği güçlü ve
uzaktan tanınabilir olmalıdır.**

Bir silahın başarılı olması, üzerinde çok fazla detay bulunmasına bağlı
değildir. Başarılı model; ilk bakışta tanınan, oyunun dünyasına ait
hissedilen ve oyun kamerasında temiz görünen modeldir.

Bu belge, LOST ISLAND'ın mevcut ilk silah konseptlerinden çıkarılmış
başlangıç standardıdır. Yeni modeller ve oyun içi testler geldikçe
sürümlenerek güncellenmelidir.

------------------------------------------------------------------------

**LOST ISLAND --- 3D Art Direction**\
*Silah tasarım standardı / v1.0*
