using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        public ProductRepository(ICatalogContext catalogContext)
        {
            this._catalogContext = catalogContext?? throw new ArgumentNullException(nameof(_catalogContext));
        }

        public async Task CreateProduct(Product product)
        {
           await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p=>p.Id,Id);
            DeleteResult deleteResult= await _catalogContext.Products.DeleteOneAsync(filterDefinition);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string Id)
        {
            return await _catalogContext.Products.Find(p => p.Id == Id).FirstOrDefaultAsync();            
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(p=>true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _catalogContext.Products.Find(filterDefinition).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _catalogContext.Products.Find(filterDefinition).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            ReplaceOneResult updateResult = await _catalogContext
                                      .Products
                                      .ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount>0;
        }
    }
}
