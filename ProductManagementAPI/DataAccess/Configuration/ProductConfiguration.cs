using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagementAPI.Core.Entities.Concrete;

namespace ProductManagementAPI.DataAccess.Configuration;

/*
 * ProductConfiguration sınıfı, `Product` varlık sınıfı için yapılandırma işlemlerini sağlar.
 * Bu sınıf, `BaseEntityConfiguration<Product>` sınıfını miras alarak temel yapılandırmaları genişletir.
 * Bu yapılandırma, ürünle ilgili veritabanı sütunlarının, ilişkilerin ve indekslerin oluşturulmasını sağlar.
 *
 * Metotlar:
 * - Configure: `Product` varlığını veritabanı için yapılandırır. `Product` varlık sınıfının
 *   tablosunun adı, gerekli alanların özellikleri ve indeksler gibi işlemleri içerir.
 *
 * Yapılandırma:
 * - builder.ToTable("Products"): Varlık `Products` adında bir tabloya karşılık gelir.
 * - builder.Property(p => p.Name): `Name` alanı zorunlu hale getirilir ve en fazla 200 karakter uzunluğunda olabilir.
 * - builder.Property(p => p.Price): `Price` alanı zorunlu hale getirilir ve `decimal(18,2)` veri tipine sahip olur.
 * - builder.Property(p => p.StockQuantity): `StockQuantity` alanı zorunlu hale getirilir.
 * - builder.Property(p => p.CreatedAt): `CreatedAt` alanı zorunlu hale getirilir ve varsayılan olarak şu anki zaman
 *   (`CURRENT_TIMESTAMP`) ile doldurulur.
 * - builder.Property(p => p.UpdatedAt): `UpdatedAt` alanı opsiyonel hale getirilir (null olabilir).
 * - builder.Property(p => p.IsActive): `IsActive` alanı zorunlu hale getirilir ve varsayılan olarak `true` değeri
 *   atanır.
 * - builder.HasIndex(p => p.Name): `Name` alanı üzerinde bir indeks oluşturulur.
 * - builder.HasIndex(p => p.Price): `Price` alanı üzerinde bir indeks oluşturulur.
 * - builder.HasIndex(p => p.StockQuantity): `StockQuantity` alanı üzerinde bir indeks oluşturulur.
 */
public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.ToTable("Products");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.StockQuantity)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(p => p.UpdatedAt)
            .IsRequired(false);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
        
        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.Price);
        builder.HasIndex(p => p.StockQuantity);
    }
}