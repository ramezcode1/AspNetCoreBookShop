﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Areas.Admin.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly BookStore.Data.ApplicationDbContext _context;

        public CreateModel(BookStore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BookCategoryId"] = new SelectList(_context.BookCategories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Books == null || Book == null)
            {
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
