using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPageTask.Data;
using RazorPageTask.Model;

namespace RazorPageTask.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly RazorPageTask.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EditModel(RazorPageTask.Data.ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product =  await _context.products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
           ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, IFormFile? file)
        {
            string wwRootPath = _hostEnvironment.WebRootPath;
            if (id != null)
            {
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    if (Product.Image != null)
                    {
                        var oldImagePath = Path.Combine(wwRootPath, Product.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var filestreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(filestreams);
                    }
                    Product.Image = @"\images\products\" + filename + extension;
                }
                _context.products.Any(u => u.ProductId == id);
                _context.products.Update(Product);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
