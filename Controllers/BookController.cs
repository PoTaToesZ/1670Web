using FPTBookStore.Data;
using FPTBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = context.Book
                .Include(s => s.Author)
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(await book);
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
            ViewBag.Author = authors;
            ViewBag.Category = categories;
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
        
        //search book
        public async Task<IActionResult> Filter(string searchString)
        {
            var allBooks = await context.Book.ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                var filteredResult = allBooks.Where(s => s.Title.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }
            return View("Index", allBooks);

        }
    }
}
