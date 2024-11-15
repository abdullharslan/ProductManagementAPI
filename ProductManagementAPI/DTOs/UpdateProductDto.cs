namespace ProductManagementAPI.DTOs;

/*
 * UpdateProductDto sınıfı, ürün güncellemeleri için kullanılan veri taşıma nesnesidir.
 * Bu DTO, bir ürünün mevcut bilgilerini güncellemek amacıyla API'ye gönderilen veriyi temsil eder.
 *
 * Özellikler:
 * - Id: Güncellenmek istenen ürünün benzersiz kimlik numarası.
 * - Name: Ürünün yeni adı.
 * - Price: Ürünün yeni fiyatı.
 * - StockQuantity: Ürünün yeni stok miktarı.
 * - IsActive: Ürünün yeni aktiflik durumu.
 */
public class UpdateProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
}