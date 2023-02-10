using RazorPageTask.Data;
using RazorPageTask.Model;
using RazorPageTask.Repository.IRepository;

namespace RazorPageTask.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChangesAsync();
        }

        public void Update(Product Product)
        {
            _db.products.Update(Product);
        }
    }
}
