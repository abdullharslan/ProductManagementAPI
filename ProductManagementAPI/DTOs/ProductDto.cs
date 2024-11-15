namespace ProductManagementAPI.DTOs;

/*
 * ProductDto sınıfı, ürün bilgilerini API ile dışarıya iletmek için kullanılan bir veri taşıma nesnesidir.
 * Bu DTO, ürünle ilgili temel verilerin dışa aktarılmasını sağlar ve genellikle veri çekme (read) işlemleri için kullanılır.
 *
 * Özellikler:
 * - Id: Ürünün benzersiz kimlik numarası.
 * - Name: Ürünün adı.
 * - Price: Ürünün fiyatı.
 * - StockQuantity: Ürünün mevcut stok miktarı.
 * - CreatedAt: Ürünün oluşturulma tarihi.
 * - UpdatedAt: Ürünün son güncellenme tarihi (isteğe bağlı).
 * - IsActive: Ürünün aktif olup olmadığı bilgisini belirtir.
 */
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}