using FPTBookStore.Data;
using FPTBookStore.Data.Cart;
using FPTBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FPTBookStore.Controllers
{
    public class OrderController : Controller
    {
        public readonly ApplicationDbContext context;
        public readonly ShoppingCart shoppingcart;
        public OrderController(ApplicationDbContext context, ShoppingCart shoppingcart)
        {
            this.context = context;
            this.shoppingcart = shoppingcart;
        }
        
        public IActionResult ShoppingCart()
        {

            var items = shoppingcart.GetShoppingCartItems();
            shoppingcart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = shoppingcart,
                ShoppingCartTotal = shoppingcart.GetShoppingCartTotal()
            };      
            return View(response);
        }

        //Add to shopping cart
        public async Task<IActionResult> AddItemToShoppingCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            shoppingcart.AddItemToCart(book);
            return RedirectToAction("ShoppingCart");
        }
        //Remove item from cart
        public async Task<IActionResult> RemoveItemFromCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            shoppingcart.RemoveItemFromCart(book);
            return RedirectToAction("ShoppingCart");
        }
        
    }
}
