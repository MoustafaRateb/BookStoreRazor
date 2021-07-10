using BookStoreRazor.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BookStoreRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly BookDbContext _db;
        public BookController(BookDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _db.Book.ToListAsync();
            return Json(new { data = books});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookToDelete = await _db.Book.FindAsync(id);
            if (bookToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            else
            {
                _db.Book.Remove(bookToDelete);
                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Delete successful" });
            }
        }
    }
}
