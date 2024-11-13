using ProductManagementAPI.Core.Entities.Abstract;

namespace ProductManagementAPI.Core.Entities.Concrete;

/*
 * Product sınıfı, bir ürünün temel özelliklerini temsil eden bir varlık sınıfıdır ve IEntity arayüzünden türetilmiştir.
 * Ürün yönetimi ve envanter takibi için gerekli olan verileri içerir.
 *
 * Özellikler:
 * - Id: Ürüne özgü benzersiz kimlik tanımlayıcısı.
 * - Name: Ürünün adı.
 * - Price: Ürünün fiyatı.
 * - StockQuantity: Ürünün stok miktarı.
 * - CreatedAt: Ürünün oluşturulma tarihi ve saati.
 * - UpdatedAt: Ürünün güncellenme tarihi ve saati (isteğe bağlı).
 * - IsActive: Ürünün aktif olup olmadığını belirten durum bilgisi.
 *
 * Bu sınıf, ürünlerin yönetimi ve envanter kontrolü için gereken temel bilgileri sağlar.
 * Veritabanı işlemlerinde uyumlu bir yapı sunar ve her ürün kaydının etkinlik durumu ve güncellenme geçmişini takip
 * edebilme imkanı verir.
 */
public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}