using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageTask.Data;
using RazorPageTask.Model;

namespace RazorPageTask.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly RazorPageTask.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreateModel(RazorPageTask.Data.ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string filename = Guid.NewGuid().ToString();
            var upload = Path.Combine(wwwRootPath, @"images\products");
            var extension = Path.GetExtension(file.FileName);
            using(var filestreams = new FileStream(Path.Combine(upload,filename + extension), FileMode.Create))
            {
                file.CopyTo(filestreams);
            }
            Product.Image = @"\images\products\" + filename + extension;
            _context.products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
