namespace ProductManagementAPI.DTOs;

/*
 * CreateProductDto sınıfı, yeni bir ürün oluşturulurken gerekli olan bilgileri taşır. Bu DTO,
 * kullanıcıdan veya dış bir kaynaktan alınan verilerin API'ye iletilmesi ve işlenmesi için kullanılır.
 * Kullanıcı bu bilgileri sağlayarak sistemde yeni bir ürün yaratabilir.
 *
 * Özellikler:
 * - Name: Ürünün adı.
 * - Price: Ürünün fiyatı.
 * - StockQuantity: Ürünün mevcut stok miktarı.
 */
public class CreateProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}