using FPTBookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPTBookStore.Data.Cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext context { get; set; }
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(ApplicationDbContext context)
        {
            this.context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Book).ToList());
        }

        public double GetShoppingCartTotal() => context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Book.Price * n.Amount).Sum();

        public void AddItemToCart(Book book)
        {
            var shoppingCartItem = context.ShoppingCartItem.FirstOrDefault(n => n.Book.Id == book.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Book = book,
                    Amount = 1
                };

                context.ShoppingCartItem.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            context.SaveChanges();
        }

        public void RemoveItemFromCart(Book book)
        {
            var shoppingCartItem = context.ShoppingCartItem.FirstOrDefault(n => n.Book.Id == book.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    context.ShoppingCartItem.Remove(shoppingCartItem);
                }
            }
            context.SaveChanges();
        }

    }
}
