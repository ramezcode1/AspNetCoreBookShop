using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BookStore.Areas.Admin.Pages.BookCategories
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly BookStore.Data.ApplicationDbContext _context;

        public EditModel(BookStore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookCategory BookCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            var bookcategory =  await _context.BookCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (bookcategory == null)
            {
                return NotFound();
            }
            BookCategory = bookcategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BookCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookCategoryExists(BookCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookCategoryExists(int id)
        {
          return (_context.BookCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
