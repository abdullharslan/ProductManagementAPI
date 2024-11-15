using ProductManagementAPI.Core.Entities.Abstract;

namespace ProductManagementAPI.DataAccess.Abstract;

/*
 * IEntityRepository arayüzü, genel veri erişim işlemleri için kullanılan temel metotları tanımlar.
T tipindeki sınıflara (varlık sınıfları) uygulandığında, CRUD (Oluşturma, Okuma, Güncelleme, Silme) işlemlerini
 * standartlaştırır. Bu yapı, uygulamanın genel veri erişim katmanında tutarlılık sağlar.
 *
 * T Sınıfı Kısıtlamaları:
 * - T: Bir varlık sınıfı olmalıdır (IEntity arayüzünden türemeli) ve bir parametresiz yapıcıya sahip olmalıdır.
 *
 * Metotlar:
 * - GetAllAsync: Tüm varlıkları asenkron olarak döndürür.
 * - GetByIdAsync: Belirtilen Id ile bir varlık döndürür.
 * - AddAsync: Yeni bir varlık ekler.
 * - UpdateAsync: Mevcut bir varlığı günceller.
 * - DeleteAsync: Belirtilen Id ile bir varlığı siler.
 * - ExistsAsync: Belirtilen Id ile bir varlığın var olup olmadığını kontrol eder.
 *
 * Bu arayüz, veri erişim operasyonlarının tutarlı bir şekilde yönetilmesini sağlamak amacıyla yapılandırılmıştır.
 */
public interface IEntityRepository<T> where T : class, IEntity, new()
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}