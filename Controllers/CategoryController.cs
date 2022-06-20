using FPTBookStore.Data;
using FPTBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FPTBookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext context;
        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Total"] = await context.Category.CountAsync();
            return View(await context.Category.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Category.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await context.Category
                .Include(b => b.Books)
                .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(category);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = context.Category.Find(id);
            context.Category.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = context.Category.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Update(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
