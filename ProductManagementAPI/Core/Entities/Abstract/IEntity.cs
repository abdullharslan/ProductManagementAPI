namespace ProductManagementAPI.Core.Entities.Abstract;

/*
 * IEntity arayüzü, tüm varlıklar için temel bir kimlik tanımlayıcısı (Id) sağlayan soyut bir yapıdır.
 * Bu arayüz, veritabanı işlemlerinde ortak bir ID alanına sahip olması gereken tüm varlıkların birbiriyle uyumlu bir
 * şekilde çalışabilmesini sağlar.
 *
 * Özellikler:
 * - Id: Her varlığa özgü birincil kimlik tanımlayıcısı.
 *
 * Bu arayüz, diğer tüm varlık sınıflarında temel bir yapı olarak kullanılarak, veritabanı işlemlerinin bir standart
 * çerçevesinde yönetilmesine olanak tanır.
 */
public interface IEntity
{
    public int Id { get; set; }
}