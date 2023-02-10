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
    public class DeleteModel : PageModel
    {
        private readonly RazorPageTask.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPageTask.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }
            var product = await _context.products.FindAsync(id);

            if (product != null)
            {
                Product = product;
                _context.products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
