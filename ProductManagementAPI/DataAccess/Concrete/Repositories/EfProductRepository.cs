using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Core.Entities.Concrete;
using ProductManagementAPI.DataAccess.Abstract;
using ProductManagementAPI.DataAccess.Abstract.Repositories;
using ProductManagementAPI.DataAccess.Context;

namespace ProductManagementAPI.DataAccess.Concrete;

/*
 * EfProductRepository sınıfı, Entity Framework Core kullanarak ürünlere yönelik CRUD ve özel sorgu işlemleri gerçekleştiren bir repository'dir.
 * Bu sınıf, ürün veritabanı işlemlerini yönetir ve ürünlerin eklenmesi, güncellenmesi, silinmesi, arama, filtreleme ve istatistiksel işlemleri gibi işlevsellikler sunar.
 * Ayrıca, toplu işlemler, sıralama ve tarih bazlı sorgulamalar gibi gelişmiş özelliklere sahiptir.
 *
 * Özellikler:
 * - Uygulama veritabanındaki ürünlerle ilgili CRUD işlemleri gerçekleştiren metodlar.
 * - Ürünlerin fiyat, stok, aktiflik durumlarına göre filtrelenmesine ve sıralanmasına olanak sağlar.
 * - Toplu fiyat güncelleme, stok güncelleme ve istatistiksel sorgulama işlemleri sunar.
 */
public class EfProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    // Constructor, veritabanı konteksi (ApplicationDbContext) ile repository sınıfını başlatır.
    public EfProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Tüm ürünleri asenkron olarak getirir
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    // ID'ye göre ürün getirir
    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    // Yeni bir ürün ekler
    public async Task<Product> AddAsync(Product entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.IsActive = true;
        await _context.Products.AddAsync(entity);
        return entity;
    }

    // Varolan bir ürünü günceller
    public async Task UpdateAsync(Product entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Products.Update(entity);
    }

    // Ürünü siler (Aktiflik durumu pasif yapılır)
    public async Task DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if (product != null)
        {
            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Update(product);
        }
    }

    // ID'ye göre ürünün var olup olmadığını kontrol eder
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(e => e.Id == id);
    }

    // Aktif ürünleri getirir
    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _context.Products.Where(p => p.IsActive).ToListAsync();
    }

    // Pasif ürünleri getirir
    public async Task<IEnumerable<Product>> GetInactiveProductsAsync()
    {
        return await _context.Products.Where(p => p.IsActive == false).ToListAsync();
    }

    // Fiyat aralığındaki ürünleri getirir
    public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _context.Products
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .ToListAsync();
    }

    // Fiyatın üstündeki ürünleri getirir
    public async Task<IEnumerable<Product>> GetProductsAbovePriceAsync(decimal price)
    {
        return await _context.Products
            .Where(p => p.Price >= price && p.IsActive)
            .ToListAsync();
    }

    // Fiyatın altındaki ürünleri getirir
    public async Task<IEnumerable<Product>> GetProductsBelowPriceAsync(decimal price)
    {
        return await _context.Products
            .Where(p => p.Price <= price && p.IsActive)
            .ToListAsync();
    }

    // Stok miktarının belirli bir eşik değerinin altında olan ürünleri getirir
    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold)
    {
        return await _context.Products
            .Where(p => p.StockQuantity <= threshold && p.IsActive)
            .ToListAsync();
    }

    // Stokta olmayan ürünleri getirir
    public async Task<IEnumerable<Product>> GetOutOfStockProductsAsync()
    {
        return await _context.Products
            .Where(p => p.StockQuantity == 0 && p.IsActive)
            .ToListAsync();
    }

    // Belirli bir tarih aralığında oluşturulan ürünleri getirir
    public async Task<IEnumerable<Product>> GetProductsCreatedBetweenDatesAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Products
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate && p.IsActive)
            .ToListAsync();
    }

    // Son eklenen ürünleri getirir (belirli bir gün sayısına göre)
    public async Task<IEnumerable<Product>> GetRecentlyAddedProductsAsync(int days)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-days);
        return await _context.Products
            .Where(p => p.CreatedAt >= cutoffDate && p.IsActive)
            .ToListAsync();
    }

    // Son güncellenen ürünleri getirir (belirli bir gün sayısına göre)
    public async Task<IEnumerable<Product>> GetRecentlyUpdatedProductsAsync(int days)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-days);
        return await _context.Products
            .Where(p => p.UpdatedAt >= cutoffDate && p.IsActive)
            .ToListAsync();
    }

    // Ürün adını arar
    public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string searchTerm)
    {
        return await _context.Products
            .Where(p => p.Name.Contains(searchTerm) && p.IsActive)
            .ToListAsync();
    }

    // Ürünleri belirtilen alana göre sıralar (örneğin: isim, fiyat, stok, oluşturulma tarihi)
    public async Task<IEnumerable<Product>> GetProductsWithSortingAsync(string sortBy, bool ascending = true)
    {
        var query = _context.Products.Where(p => p.IsActive);
        
        query = sortBy.ToLower() switch
        {
            "name" => ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
            "price" => ascending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
            "stock" => ascending ? query.OrderBy(p => p.StockQuantity) : query.OrderByDescending(p => p.StockQuantity),
            "createdat" => ascending ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt),
            _ => ascending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id)
        };
        
        return await query.ToListAsync();
    }

    // Tüm ürünlerin fiyatlarını topluca günceller
    public async Task<bool> BulkUpdatePricesAsync(decimal percentage, bool increase = true)
    {
        var products = await _context.Products.Where(p => p.IsActive).ToListAsync();
        foreach (var product in products)
        {
            product.Price = increase
                ? product.Price * (1 + percentage / 100)
                : product.Price * (1 - percentage / 100);
            product.UpdatedAt = DateTime.UtcNow;
        }

        return true;
    }

    // Belirli ürünlerin stok miktarını topluca günceller
    public async Task<bool> BulkUpdateStockAsync(int[] productIds, int quantity)
    {
        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id) && p.IsActive)
            .ToListAsync();

        foreach (var product in products)
        {
            product.StockQuantity = quantity;
            product.UpdatedAt = DateTime.UtcNow;
        }
        
        return true;
    }

    // Aktif ürünlerin ortalama fiyatını hesaplar
    public async Task<decimal> GetAveragePriceAsync()
    {
        return await _context.Products
            .Where(p => p.IsActive)
            .AverageAsync(p => p.Price);
    }

    // Tüm aktif ürünlerin toplam stok miktarını hesaplar
    public async Task<int> GetTotalStockQuantityAsync()
    {
        return await _context.Products
            .Where(p => p.IsActive)
            .SumAsync(p => p.StockQuantity);
    }

    // Aktif ürünlerin toplam sayısını getirir
    public async Task<int> GetActiveProductCountAsync()
    {
        return await _context.Products.CountAsync(p => p.IsActive);
    }

    // Ürün adı benzersiz mi diye kontrol eder
    public async Task<bool> IsProductNameUniqueAsync(string name)
    {
        return await _context.Products
            .AnyAsync(p => p.Name == name && p.IsActive);
    }

    // Ürünün yeterli stoğa sahip olup olmadığını kontrol eder
    public async Task<bool> HasSufficientStockAsync(int productId, int requestedQuantity)
    {
        var product = await GetByIdAsync(productId);
        return product != null && product.StockQuantity >= requestedQuantity;
    }
}