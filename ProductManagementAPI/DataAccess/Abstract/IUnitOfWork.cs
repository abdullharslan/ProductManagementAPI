using ProductManagementAPI.DataAccess.Abstract.Repositories;

namespace ProductManagementAPI.DataAccess.Abstract;

/*
 * IUnitOfWork arayüzü, birim-i-iş (Unit of Work) tasarım desenine uygun olarak,
 * veri erişim işlemlerini bir bütün halinde yönetmek için kullanılır. 
 * Bu desen, özellikle birden fazla veri tabanı tablosuna yapılan değişikliklerin
 * aynı işlemde topluca kaydedilmesi veya geri alınması gerektiği durumlarda kullanışlıdır.
 *
 * Amacı:
 * - Veritabanı işlemlerinin tutarlı bir şekilde gerçekleştirilmesini sağlamak.
 * - Farklı repository'ler arasında koordinasyonu sağlamak.
 * - Veritabanına yapılan değişikliklerin tek bir işlem olarak yönetilmesine olanak tanımak.
 * - Kaynakların etkili bir şekilde serbest bırakılmasını sağlamak.
 *
 * Özellikler:
 * - **Product**: Ürün yönetimi için özel veri erişim işlemlerini sağlayan `IProductRepository` nesnesine erişim sunar.
 * 
 * Metotlar:
 * - **SaveChangesAsync**: 
 *   - Tüm değişiklikleri tek bir işlemde veritabanına kaydeder.
 *   - Hata durumunda değişikliklerin geri alınmasını sağlar.
 *   - Geri dönen `int` değeri, etkilenen kayıt sayısını ifade eder.
 *
 * IDisposable Arayüzü:
 * - IDisposable, kaynakların serbest bırakılması için kullanılan bir arayüzdür.
 * - `Dispose` metodu, bellekte yer kaplayan kaynakları (örneğin, bir veritabanı bağlantısı) serbest bırakır.
 * - IDisposable'ın uygulanması, özellikle aşağıdaki durumlarda gereklidir:
 *     1. Veri tabanı bağlantılarının kapatılması.
 *     2. İşlem sırasında açılan dosyaların serbest bırakılması.
 *     3. Yüksek bellek kullanımı gerektiren nesnelerin temizlenmesi.
 * - Örnek kullanım:
 *   ```
 *   using (var unitOfWork = new UnitOfWork())
 *   {
 *       // İşlem yapılır
 *   }
 *   // using bloğu bittiğinde, Dispose metodu otomatik olarak çağrılır.
 *   ```
 *
 * Kullanım Senaryoları:
 * - Aynı işlem zincirinde birden fazla tabloya güncelleme yapılması gerektiğinde.
 * - Hata durumunda yapılan değişikliklerin geri alınması gerektiğinde (Transaction yönetimi).
 * - Farklı repository'ler üzerinde gerçekleştirilen işlemlerin bir arada yönetilmesi gerektiğinde.
 *
 * Örnek Senaryo:
 * ```
 * using (var unitOfWork = new UnitOfWork())
 * {
 *     var newProduct = new Product { Name = "Laptop", Price = 1500 };
 *     await unitOfWork.Product.AddAsync(newProduct);
 *     
 *     // Tüm işlemleri veritabanına kaydet
 *     await unitOfWork.SaveChangesAsync();
 * }
 * ```
 *
 * IUnitOfWork arayüzü, kodun daha düzenli, test edilebilir ve bakımının kolay yapılabilir bir yapıya kavuşmasını sağlar.
 */
public interface IUnitOfWork : IDisposable
{
    IProductRepository Product { get; }
    Task<int> SaveChangesAsync();
}