using ProductManagementAPI.Core.Entities.Concrete;

namespace ProductManagementAPI.DTOs.Extensions;

/*
 * ProductMappingExtensions sınıfı, `Product` varlık sınıfı ile `ProductDto`, `CreateProductDto`, ve `UpdateProductDto`
 * veri transfer objeleri (DTO'lar) arasındaki dönüşüm işlemlerini tanımlar. Bu uzantı metotları, varlık sınıflarını
 * DTO'lara ve tam tersi işlemleri kolayca yapabilmeyi sağlar.
 *
 * Metotlar:
 * - ToDto: `Product` varlık sınıfını bir `ProductDto` nesnesine dönüştürür.
 * - ToEntity: `CreateProductDto` nesnesini bir `Product` varlık nesnesine dönüştürür.
 * - UpdateEntity: `UpdateProductDto` nesnesini mevcut bir `Product` varlık nesnesine uygular ve günceller.
 */
public static class ProductMappingExtensions
{
    /*
     * ToDto metodu, `Product` varlık sınıfını `ProductDto` veri transfer objesine dönüştürür.
     * Bu dönüşüm, verilerin dışa aktarılması veya API yanıtlarında kullanılması için yapılır.
     *
     * Parametreler:
     * - product: Dönüştürülmesi gereken `Product` varlık nesnesi.
     *
     * Dönen Değer:
     * - `ProductDto`: Dönüştürülmüş `Product` varlık nesnesi.
     */
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            IsActive = product.IsActive
        };
    }

    /*
     * ToEntity metodu, `CreateProductDto` veri transfer objesini `Product` varlık sınıfına dönüştürür.
     * Bu dönüşüm, yeni bir ürün oluşturulurken veritabanına kaydetmek için yapılır.
     *
     * Parametreler:
     * - createProductDto: Dönüştürülmesi gereken `CreateProductDto` nesnesi.
     *
     * Dönen Değer:
     * - `Product`: Dönüştürülmüş `Product` varlık nesnesi.
     */
    public static Product ToEntity(this CreateProductDto createProductDto)
    {
        return new Product
        {
            Name = createProductDto.Name,
            Price = createProductDto.Price,
            StockQuantity = createProductDto.StockQuantity,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /*
     * UpdateEntity metodu, `UpdateProductDto` veri transfer objesini mevcut bir `Product` varlık nesnesine uygular.
     * Bu işlem, varlık nesnesini güncellemek için yapılır.
     *
     * Parametreler:
     * - updateProductDto: Dönüştürülmesi gereken `UpdateProductDto` nesnesi.
     * - product: Güncellenecek `Product` varlık nesnesi.
     */
    public static void UpdateEntity(this UpdateProductDto updateProductDto, Product product)
    {
        product.Name = updateProductDto.Name;
        product.Price = updateProductDto.Price;
        product.StockQuantity = updateProductDto.StockQuantity;
        product.IsActive = updateProductDto.IsActive;
        product.UpdatedAt = DateTime.UtcNow;
    }
}