using ProductManagementAPI.Core.Entities.Concrete;

namespace ProductManagementAPI.Core.Repositories.Abstract;

/*
 * IProductRepository arayüzü, ürün yönetimi için gereken veri erişim işlemlerini sağlayan metotları tanımlar.
 * Bu arayüz, ürünlerin fiyat, stok durumu, tarih bazlı sorgular, arama, toplu güncellemeler ve istatistiksel analizler
 * gibi çeşitli alanlarda sorgulanmasını mümkün kılar.
 *
 * Genel Metot Kategorileri:
 * - Aktif/Pasif Ürün Sorguları:
 *     - GetActiveProductsAsync: Aktif olan ürünleri döndürür.
 *     - GetInactiveProductsAsync: Pasif olan ürünleri döndürür.
 * - Fiyat Bazlı Sorgular:
 *     - GetProductsByPriceRangeAsync: Belirtilen fiyat aralığındaki ürünleri döndürür.
 *     - GetProductsAbovePriceAsync: Belirtilen fiyatın üzerindeki ürünleri döndürür.
 *     - GetProductsBelowPriceAsync: Belirtilen fiyatın altındaki ürünleri döndürür.
 * - Stok Bazlı Sorgular:
 *     - GetLowStockProductsAsync: Stok seviyesi belirtilen eşiğin altında olan ürünleri döndürür.
 *     - GetOutOfStockProductsAsync: Stokta olmayan ürünleri döndürür.
 * - Tarih Bazlı Sorgular:
 *     - GetProductsCreatedBetweenDatesAsync: Belirtilen tarihler arasında eklenen ürünleri döndürür.
 *     - GetRecentlyAddedProductsAsync: Belirtilen gün sayısı içinde eklenen ürünleri döndürür.
 *     - GetRecentlyUpdatedProductsAsync: Belirtilen gün sayısı içinde güncellenen ürünleri döndürür.
 * - Arama ve Filtreleme:
 *     - SearchProductsByNameAsync: Ürün ismine göre arama yapar.
 *     - GetProductsWithSortingAsync: Ürünleri belirli bir kritere göre sıralar.
 * - Toplu İşlemler:
 *     - BulkUpdatePricesAsync: Belirtilen yüzdeye göre ürün fiyatlarını topluca günceller.
 *     - BulkUpdateStockAsync: Belirtilen ürünlerin stok miktarını topluca günceller.
 * - İstatistiksel Sorgular:
 *     - GetAveragePriceAsync: Ürünlerin ortalama fiyatını döndürür.
 *     - GetTotalStockQuantityAsync: Tüm ürünlerin toplam stok miktarını döndürür.
 *     - GetActiveProductCountAsync: Aktif ürünlerin sayısını döndürür.
 * - Özel Durum Kontrolleri:
 *     - IsProductNameUniqueAsync: Ürün adının benzersiz olup olmadığını kontrol eder.
 *     - HasSufficientStockAsync: Belirtilen ürün için yeterli stok olup olmadığını kontrol eder.
 *
 * Bu arayüz, ürünlerin yönetimi ve analizi için kapsamlı bir veri erişim yapısı sunar.
 */
public interface IProductRepository : IEntityRepository<Product>
{
    // Aktif/Pasif ürün sorguları
    Task<IEnumerable<Product>> GetActiveProductsAsync();
    Task<IEnumerable<Product>> GetInactiveProductsAsync();
    
    // Fiyat bazlı sorgular
    Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<IEnumerable<Product>> GetProductsAbovePriceAsync(decimal price);
    Task<IEnumerable<Product>> GetProductsBelowPriceAsync(decimal price);
    
    // Stok bazlı sorgular
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
    Task<IEnumerable<Product>> GetOutOfStockProductsAsync();
    
    // Tarih bazlı sorgular
    Task<IEnumerable<Product>> GetProductsCreatedBetweenDatesAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Product>> GetRecentlyAddedProductsAsync(int days);
    Task<IEnumerable<Product>> GetRecentlyUpdatedProductsAsync(int days);
    
    // Arama ve filtreleme
    Task<IEnumerable<Product>> SearchProductsByNameAsync(string searchTerm);
    Task<IEnumerable<Product>> GetProductsWithSortingAsync(string sortBy, bool ascending = true);
    
    // Toplu işlemler
    Task<bool> BulkUpdatePricesAsync(decimal percentage, bool increase = true);
    Task<bool> BulkUpdateStockAsync(int[] productIds, int quantity);
    
    // İstatistiksel sorgular
    Task<decimal> GetAveragePriceAsync();
    Task<int> GetTotalStockQuantityAsync();
    Task<int> GetActiveProductCountAsync();
    
    // Özel durum kontrolleri
    Task<bool> IsProductNameUniqueAsync(string name);
    Task<bool> HasSufficientStockAsync(int productId, int requestedQuantity);
}