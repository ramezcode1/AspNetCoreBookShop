using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BookStore.Areas.Admin.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly BookStore.Data.ApplicationDbContext _context;

        public IndexModel(BookStore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books
                .Include(b => b.BookCategory).ToListAsync();
            }
        }
    }
}
