using RazorPageTask.Model;

namespace RazorPageTask.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product Product);
        void Save();
    }
}
