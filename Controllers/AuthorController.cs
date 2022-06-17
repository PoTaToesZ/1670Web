using FPTBookStore.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FPTBookStore.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FPTBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private ApplicationDbContext context;

        public AuthorController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewData["Total"] = await context.Author.CountAsync();
            return View(await context.Author.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Author.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await context.Author
                .Include(b => b.Books)
                .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(author);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = context.Author.Find(id);
            context.Author.Remove(author);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = context.Author.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Update(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }
    }
}
