using ProductManagementAPI.DataAccess.Abstract;
using ProductManagementAPI.DataAccess.Abstract.Repositories;
using ProductManagementAPI.DataAccess.Concrete.Repositories;
using ProductManagementAPI.DataAccess.Context;

namespace ProductManagementAPI.DataAccess.Concrete;

/*
 * UnitOfWork sınıfı, "Unit of Work" tasarım desenini uygulayarak, veri erişim işlemlerini bir bütün olarak yönetir.
 * Bu sınıf, özellikle birden fazla repository (veri erişim katmanı) ile çalışan ve bu repository'lerdeki işlemleri 
 * tek bir işlemde (transaction) kaydetmek isteyen uygulamalarda kullanılır. UnitOfWork, işlem sınırlarını (transaction) 
 * yönetmek ve tüm değişikliklerin tek bir "SaveChanges" ile kaydedilmesini sağlamak amacıyla kullanılır.
 * 
 * IUnitOfWork arayüzünü implement eden bu sınıf, birden fazla repository'nin ve ilgili işlemlerin yönetilmesini sağlar.
 * 
 * Özellikler:
 * - **_context**: ApplicationDbContext nesnesi, veritabanı ile etkileşim için kullanılır.
 * - **_productRepository**: Ürünlere ait veri erişim işlemleri için kullanılan `IProductRepository` nesnesi.
 * - **_disposed**: Kaynakları serbest bırakıp bırakılmadığını kontrol eden bayrak.
 *
 * Metotlar:
 * - **Product**: 
 *   - Ürünler ile ilgili veri erişim işlemlerini sağlamak için `IProductRepository` nesnesini döndüren özelliktir.
 *   - Lazy Loading kullanılarak, ihtiyaç duyulduğunda repository oluşturulur.
 *
 * - **SaveChangesAsync**: 
 *   - Uygulamanın veritabanı üzerinde gerçekleştirdiği değişiklikleri asenkron bir şekilde kaydeder.
 *   - Bu metot, yapılan tüm işlemleri birleştirip veritabanına gönderir.
 *
 * - **Dispose**:
 *   - Bu metot, IDisposable arayüzü tarafından çağrılır ve sınıfın kullandığı kaynakları (özellikle veritabanı
 *     bağlamını) serbest bırakır.
 *   - Dispose metodu çağrıldığında, `ApplicationDbContext` nesnesi ve ilişkili kaynaklar düzgün bir şekilde serbest
 *     bırakılır.
 *
 * - **Dispose(bool disposing)**:
 *   - Bu metot, Dispose işlevinin ayrıntılı bir implementasyonudur ve kaynakların serbest bırakılmasında kullanılır.
 *   - Parametreli versiyon, belleği doğru şekilde temizlerken, yalnızca 'disposing' parametresi true olduğunda
 *     kaynakları temizler.
 *
 * **IDisposable Arayüzü**:
 * - IDisposable, yönetilmeyen kaynakları (örneğin, veritabanı bağlantıları) serbest bırakmak için kullanılır.
 * - Bu arayüzü implement eden sınıflar, uygulama yaşam döngüsünde gereksiz bellek ve kaynak kullanımını önler.
 * - Dispose metodu, sistemin kaynakları yönetmesine yardımcı olur ve bellek sızıntılarının önüne geçer.
 *
 * **UnitOfWork Tasarım Deseni**:
 * - Birden fazla repository üzerinden yapılan işlemlerin bir bütün olarak yönetilmesini sağlar.
 * - Bir işlem (transaction) çerçevesinde tüm repository'lerdeki değişikliklerin tek bir işlem olarak kaydedilmesine
 *   olanak tanır.
 * - Bu sınıf, özellikle işlemlerin başarılı bir şekilde tamamlanması gerektiğinde ve hata durumunda tüm değişikliklerin
 *   geri alınması gerektiğinde kullanılır.
 * 
 * Bu kullanımda, tüm işlemler bir bütün olarak yönetilir. Eğer bir hata meydana gelirse, değişiklikler geri alınabilir.
 */
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IProductRepository _productRepository;
    // Kaynakların serbest bırakılıp bırakılmadığını takip etmek için bayrak
    private bool _disposed;  
    
    // Constructor metod, veritabanı bağlamını alır ve UnitOfWork sınıfının başlatılmasını sağlar.
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IProductRepository Product => 
        _productRepository ??= new EfProductRepository(_context);
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // IDisposable implementasyonu: Kaynakları serbest bırakmak için kullanılır.
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            // Veritabanı bağlamını serbest bırak.
            _context.Dispose();
        }
        _disposed = true;  // Dispose işleminden sonra tekrar işlem yapılmasın diye bayrağı true yapıyoruz.
    }

    // IDisposable implementasyonu: Dispose metodunu çağırır.
    public void Dispose()
    {
        Dispose(true);  // Kaynakları serbest bırak
        GC.SuppressFinalize(this);  // Finalizer'ın çalışmamasını sağlar.
    }
}