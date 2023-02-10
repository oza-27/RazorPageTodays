using Microsoft.EntityFrameworkCore;
using RazorPageTask.Model;

namespace RazorPageTask.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
		{

		}
		public DbSet<Category> categories { get; set; }
		public DbSet<Product> products { get; set; }
	}
}
