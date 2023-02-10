using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageTask.Data;

namespace RazorPageTask.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Model.Category> Categories { get; set; }
        public IEnumerable<Model.Product> Products { get; set; }
        public void OnGet(string[]? catName, string? productName,int id)
        {
            if (catName != null & catName.Length > 0)
            {
                Categories = _db.categories.Where(i => catName.Contains(i.Name)).ToList();
                Products = _db.products;
            }
            else
            {
                if (!string.IsNullOrEmpty(productName))
                {
                    ViewData[productName] = productName;
                    Categories = _db.products.Include(p => p.Category).Where(p => p.Name == productName)
                        .Select(p => p.Category).ToList();
                    Products = _db.products.ToList();
                }
                else
                {
                    Categories = _db.categories;
                    var getData = _db.products.Find(id);
                    ViewData["CategoryName"] = getData.Name;
                }
            }
        }

        public async Task<IActionResult> OnPost(string searchtxt)
        {
            var prodStr = searchtxt.Split(',');
            return RedirectToPage("/Products/Index", new { prodNames = prodStr });
        }
    }
}
