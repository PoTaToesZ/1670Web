using FPTBookStore.Data;
using FPTBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext context;
        public BookController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Total"] = await context.Book.CountAsync();
            return View(await context.Book.ToListAsync());
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = context.Book
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(book);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = context.Book.Find(id);
            context.Book.Remove(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var authors = context.Author.ToList();
            var categories = context.Category.ToList();
            ViewBag.Authors = authors;
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                context.Book.Add(book);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = context.Book.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            var authors = context.Author.ToList();
            var categories = context.Category.ToList();
            ViewBag.Authors = authors;
            ViewBag.Categories = categories;
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                context.Book.Update(book);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }
    }
}
