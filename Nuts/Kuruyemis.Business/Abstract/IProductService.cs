using Kuruyemis.DataAccess.Abstract;
using Kuruyemis.Entities.Concrete;


namespace Kuruyemis.Business.Abstract
{
    public interface IProductService
    {
        public IProductDal _productDal { get; set; }

        public Product ProductWithCategoryById(int id) => _productDal.GetProductWithCategoryById(id);
        public List<Product> ProductsWithCategory() => _productDal.ProductWithCategory();
        Task<Product> GetProductAsync(int id) => _productDal.GetAsync(x => x.Id == id);
        async Task<List<Product>> GetProductsAsync() => (await _productDal.GetAllAsync()).OrderBy(x => x.Name).ToList();
        async Task<bool> UpdateProductAsync(Product product) => await _productDal.UpdateAsync(product) > 0;
        async Task<bool> AddProductAsync(Product product) => await _productDal.AddAsync(product) > 0;
        async Task<bool> DeleteProductAsync(int productId) => await _productDal.RemoveAsync(await GetProductAsync(productId)) > 0;
    }
}
