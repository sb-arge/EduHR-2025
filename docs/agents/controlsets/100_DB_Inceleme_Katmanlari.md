# İnceleme Katmanları
## **1️⃣ Teknik Doğruluk (Low Level – Veritabanı Katmanı)**

Bu kısımda **tip, index, constraint** gibi konular var.  
Yani veritabanının _çalışabilir, güvenilir, performanslı_ olmasını sağlamak.

----------

## **2️⃣ Amaca Uygunluk & Tamlık (Mid Level – Domain/Business Uyumu)**

-   **Fonksiyonel Kapsam**
    
    -   Projenin amacına ulaşması için _tüm gerekli tablolar, alanlar ve ilişkiler_ var mı?
        
    -   Hiçbir kritik veri veya iş süreci eksik mi?
        
    -   Kullanıcı senaryolarındaki veri ihtiyaçları karşılanıyor mu?
        
-   **İş Süreci Akışı ile Uyum**
    
    -   Tablolar, gerçek hayattaki iş süreçlerine birebir oturuyor mu?
        
    -   İş adımlarına karşılık gelen kayıt geçişleri mümkün mü? (örn. sipariş → ödeme → kargo gibi)
        
    -   Gereken alanlar sürecin her aşamasında mevcut mu?
        
-   **Kural ve Mantık Uygunluğu**
    
    -   Veri tabanında iş kuralları enforce edilmiş mi? (örn. `discount_percentage` 0-100 arası olmalı)
        
    -   Sadece kod tarafına bırakılan ama DB tarafında da garanti edilmesi gereken kısıtlar var mı?
        

----------

## **3️⃣ Best Practice & Sektör Standartları ile Kıyaslama (High Level – Benchmark)**

Burada PostgreSQL + SaaS dünyasındaki _olmuş bitmiş_ tasarımlarla karşılaştırırız:

-   **Çok tenant’lı yapı (multi-tenancy)**: RLS, tenant_id, index stratejileri.
    
-   **Audit & logging**: Değişiklik takibi, soft delete alanları (`deleted_at` vb.)
    
-   **Scalability**: Büyük veri için partitioning, caching stratejileri.
    
-   **Güvenlik**: Hassas verilerin şifrelenmesi (örn. PII, tokenlar)
    
-   **Standard Columns**: Tüm CRUD tablolarında `created_at`, `updated_at`, `created_by`, `updated_by`.
    
-   **Kullanıcı & Rol Yönetimi**: Yetki modeli, role-based access kontrol tabloları.
    

----------
Yani inceleme seti şöyledir. 
| Katman                          | İnceleme Başlığı                             | Kontrol Alanları                           |
| ------------------------------- | -------------------------------------------- | ------------------------------------------ |
| 1️⃣ Teknik Doğruluk             | Tip, index, constraint, FK/Pk, Null kontrolü | Performans, veri bütünlüğü                 |
| 2️⃣ Amaca Uygunluk              | Tüm alanlar var mı, iş sürecine uyum         | Süreçlerin veri tabanında karşılığı var mı |
| 3️⃣ Best Practice Karşılaştırma | SaaS + PostgreSQL standartları               | Multi-tenancy, güvenlik, audit, scale      |