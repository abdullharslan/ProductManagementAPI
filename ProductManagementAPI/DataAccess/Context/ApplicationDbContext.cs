using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Core.Entities.Concrete;
using ProductManagementAPI.DataAccess.Configuration;

namespace ProductManagementAPI.DataAccess.Context;

/*
 * ApplicationDbContext sınıfı, Entity Framework Core kullanarak veritabanı ile etkileşimi sağlayan temel veri erişim konteksidir.
 * Bu sınıf, uygulama veritabanı üzerinde işlem yapılacak olan modellerin (entity'ler) tanımlanmasını ve yapılandırılmasını sağlar.
 *
 * Özellikler:
 * - Products: Ürünleri temsil eden DbSet, ürünler tablosuna karşılık gelir.
 *
 * OnModelCreating metodu, model yapılandırmaları ve veritabanı şeması üzerinde özelleştirmeler yapılmasını sağlar.
 * Burada, Product sınıfı için tanımlanan özel konfigürasyonlar (ProductConfiguration) uygulanır.
 */
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Veritabanındaki 'Products' tablosu ile ilişkilendirilmiş DbSet
    public DbSet<Product> Products { get; set; }

    // Model oluşturulurken yapılan özelleştirmeler ve yapılandırmalar
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 'Product' modeline ait özel yapılandırmaları uygulamak için
        modelBuilder.ApplyConfiguration(new ProductConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}