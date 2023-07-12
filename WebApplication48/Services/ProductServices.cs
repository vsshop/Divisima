using Microsoft.EntityFrameworkCore;
using WebApplication48.Models;

namespace WebApplication48.Services
{
    public class ProductServices
    {
        EntityDatabase _database;
        public ProductServices(EntityDatabase database)
        {
            _database = database;
        }
        public async Task<List<ProductModel>> GetProducts()
        {
            await Task.Delay(1000);
            using(EntityDatabase db = _database)
            {
                if(db.Products.Count() == 0)
                {
                    await db.Products.AddRangeAsync(DatabaseMoq.Products);
                    await db.SaveChangesAsync();
                }
                return await db.Products.ToListAsync();
            }
            
        }
        public async Task<ProductModel?> GetProductById(Guid productId)
        {
            using (EntityDatabase db = _database)
            {
                return await db.Products.FirstOrDefaultAsync(p => p.Id == productId);
            }
        }
    }
}
