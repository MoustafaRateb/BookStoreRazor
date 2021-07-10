using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStoreRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        BookDbContext _db;

        public IndexModel(BookDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
            
        }
        public async Task<ActionResult> OnPostDelete(int id)
        {
            var bookToDelete = await _db.Book.FindAsync(id);
            if(bookToDelete == null)
            {
                return NotFound();
            }
            else
            {
                _db.Book.Remove(bookToDelete);
                await _db.SaveChangesAsync();
                return RedirectToPage();
            }

        }
    }
}
