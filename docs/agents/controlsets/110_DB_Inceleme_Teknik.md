## **İnceleme Kategorileri ve Kontrol Noktaları**

Her satır = bir sütun tanımı gibi düşün. Tüm satırları filtreleyip gruplara ayırarak kontrol et.

### 1️⃣ **İsimlendirme & Yapı Standartları**

-   **Şema adı** tutarlı mı? (aynı işlevdeki tablolar aynı şema altında mı?)
    
-   **Tablo adları**: snake_case, anlamlı ve kısaltmasız mı?
    
-   **Sütun adları**: gereksiz prefix yok mu? (örn. `user_user_id` yerine `user_id`)
    
-   **Index adları**: `ix_{table}_{column}` gibi standart formatta mı?

-   **foreignkey adları**: `fk_{table}_{column}` gibi standart formatta mı?
    

----------

### 2️⃣ **Veri Tipi & Boyut Doğruluğu**

-   **Metin alanları**: `varchar` mı, `text` mi? Boyut kısıtlaması mantıklı mı?
    
-   **Sayısal tipler**: `integer` / `bigint` doğru seçilmiş mi? `numeric` kullanımı gerçekten gerekli mi?
    
-   **Zaman tipleri**: `timestamptz` mi kullanılıyor, yoksa timezone’suz `timestamp` mi?
    
-   **Boolean**: `smallint` gibi yanlış tipte mi tutuluyor?
    
-   **JSON/JSONB**: Gereksiz mi yoksa esnek veri için mi eklenmiş?
    

----------

### 3️⃣ **Zorunluluk ve Varsayılanlar**

-   **NOT NULL** atamaları eksik mi?
    
-   Varsayılan değer (`DEFAULT`) doğru tanımlanmış mı?
    
-   Tarih alanlarında `NOW()` veya `CURRENT_TIMESTAMP` default var mı?
    

----------

### 4️⃣ **Anahtarlar & İlişkiler**

-   Primary Key **tüm tablolarda** var mı? (id-integer)

-   publickey_id **tüm tablolarda** var mı? (uuid) 
    
-   Foreign Key’ler ilişkili tablo/sütunla eşleşiyor mu? 
    
-   Cascade davranışları (`ON DELETE CASCADE` vb.) doğru seçilmiş mi?
    
-   Unique constraint gerekli yerlerde var mı?
    

----------

### 5️⃣ **Index Tasarımı**

-   PK ve FK’ler için indexler tanımlanmış mı?
    
-   Sık sorgulanan alanlar için ek index var mı?
    
-   Index tipi (`btree`, `GIN` vb.) doğru seçilmiş mi?
    
-   Gereksiz index fazlalığı var mı? (fazla index, insert/update performansını bozar)
    
-   Partial veya covering index fırsatları var mı?
    

----------

### 6️⃣ **Veri Bütünlüğü & Kısıtlar**

-   `CHECK` constraint kullanımı var mı?
    
-   Enum mu yoksa lookup table mı tercih edilmiş?
    
-   NULL olamayacak ama NOT NULL tanımlanmamış alanlar var mı?
    

----------

### 7️⃣ **Performans & Ölçeklenebilirlik**

-   Büyük veri beklenen tablolarda **partitioning** planı var mı?
    
-   Arama ihtiyacı olan text alanlarda `tsvector` düşünülmüş mü?
    
-   Materialized view ihtiyacı olan rapor tabloları var mı?
    

----------

### 8️⃣ **Güvenlik & Çok Tenant’lı Yapı**

-   Row Level Security (RLS) ihtiyacı olan tablolarda uygulanmış mı?
    
-   Hassas veriler (`password`, `token`) şifrelenmiş mi?
    
-   Şema erişim yetkileri doğru tanımlanmış mı?