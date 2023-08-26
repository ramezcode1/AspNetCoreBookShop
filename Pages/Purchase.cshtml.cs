using BookStore.Data;
using BookStore.ExtensionMethods;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Pages
{
    [Authorize]
    public class PurchaseModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PurchaseModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CartItem> Cart { get; set; } = default!;

        public void OnGet()
        {
            Cart = HttpContext.Session.GetObject<List<CartItem>>("cart");
            if (Cart != null)
            {
                ViewData["total"] = Cart.Sum(s => s.SubTotal);
            }
            else
            {
                Cart = new List<CartItem>();
                ViewData["total"] = 0;
            }
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int? id, int quantity = 1)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            Cart = HttpContext.Session.GetObject<List<CartItem>>("cart");
            if (Cart == null)
            {
                Cart = new List<CartItem>();
                Cart.Add(new CartItem { Book = book, Quantity = quantity });
            }
            else
            {
                int index = Cart.FindIndex(c => c.Book.Id == id);
                if (index != -1) //if item already in the cart
                {
                    Cart[index].Quantity++; //increment by 1
                }
                else
                {
                    Cart.Add(new CartItem { Book = book, Quantity = quantity });
                }
            }

            HttpContext.Session.SetObject<List<CartItem>>("cart", Cart);
            return RedirectToPage("/Purchase");
        }

        // increment the quantity by 1 on + click
        public async Task<IActionResult> OnPostAddQtyAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            Cart = HttpContext.Session.GetObject<List<CartItem>>("cart");

            int index = Cart.FindIndex(c => c.Book.Id == id);
            Cart[index].Quantity++; //increment by 1

            HttpContext.Session.SetObject<List<CartItem>>("cart", Cart);
            return RedirectToPage("/Purchase");
        }

        // decrement the quantity by 1 on - click
        public async Task<IActionResult> OnPostMinusQtyAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            Cart = HttpContext.Session.GetObject<List<CartItem>>("cart");

            int index = Cart.FindIndex(c => c.Book.Id == id);
            if (Cart[index].Quantity == 1)
            {
                Cart.RemoveAt(index);
            }
            else
            {
                Cart[index].Quantity--; //reduce by 1
            }

            HttpContext.Session.SetObject<List<CartItem>>("cart", Cart);
            return RedirectToPage("/Purchase");
        }

        public async Task<IActionResult> OnPostRemoveItemAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            Cart = HttpContext.Session.GetObject<List<CartItem>>("cart");

            int index = Cart.FindIndex(c => c.Book.Id == id);
            Cart.RemoveAt(index);

            HttpContext.Session.SetObject<List<CartItem>>("cart", Cart);
            return RedirectToPage("/Purchase");
        }
    }
}
