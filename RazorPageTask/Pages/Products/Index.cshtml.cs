using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageTask.Data;
using RazorPageTask.Model;

namespace RazorPageTask.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly RazorPageTask.Data.ApplicationDbContext _context;

        public IndexModel(RazorPageTask.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.products != null)
            {
                Product = await _context.products
                .Include(p => p.Category).ToListAsync();
            }
        }
    }
}
