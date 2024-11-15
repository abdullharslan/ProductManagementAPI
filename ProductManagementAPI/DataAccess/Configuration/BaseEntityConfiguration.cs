using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagementAPI.Core.Entities.Abstract;

namespace ProductManagementAPI.DataAccess.Configuration;

/*
 * BaseEntityConfiguration sınıfı, tüm varlıklar için temel yapılandırma işlemlerini sağlar. Bu sınıf,
 * `IEntityTypeConfiguration<T>` arayüzünü uygular ve Entity Framework Core için temel yapılandırma işlemleri sağlar.
 * `T` tipi, IEntity arayüzünü implement eden her varlık sınıfı olabilir.
 *
 * Metotlar:
 * - Configure: Bu metod, tüm varlıklar için temel yapılandırmaları (Id anahtarının ayarlanması ve otomatik olarak
 *   değeri üretilen Id property'sinin ayarlanması gibi) sağlar.
 *
 * Yapılandırma:
 * - builder.HasKey(e => e.Id): Varlık için `Id` property'si anahtar olarak ayarlanır. Bu, veritabanında her varlık
 *   için benzersiz bir kimlik sağlar.
 * - builder.Property(e => e.Id).ValueGeneratedOnAdd(): `Id` property'sinin değeri, yeni bir varlık eklenirken 
 *   otomatik olarak üretilir (örneğin, veritabanı tarafından birincil anahtar için otomatik artan bir değer gibi).
 */
public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
    }
}