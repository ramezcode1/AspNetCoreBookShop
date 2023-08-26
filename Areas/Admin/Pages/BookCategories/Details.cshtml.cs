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

namespace BookStore.Areas.Admin.Pages.BookCategories
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly BookStore.Data.ApplicationDbContext _context;

        public DetailsModel(BookStore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public BookCategory BookCategory { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            var bookcategory = await _context.BookCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (bookcategory == null)
            {
                return NotFound();
            }
            else 
            {
                BookCategory = bookcategory;
            }
            return Page();
        }
    }
}
