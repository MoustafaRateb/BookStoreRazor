using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly BookDbContext _db;
        public EditModel(BookDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }
        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
        }

        public async Task<ActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var bookFromDb = await _db.Book.FindAsync(Book.Id);
                bookFromDb.Title = Book.Title;
                bookFromDb.Author = Book.Author;
                bookFromDb.ISBN = Book.ISBN;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
            
        }
    }
}
