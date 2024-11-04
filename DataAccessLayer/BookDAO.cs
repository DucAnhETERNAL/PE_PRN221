using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookDAO : SingletonBase<BookDAO>
    {
        //GetAll
        public async Task<IEnumerable<Books>> GetBookAll()
        {
            return await _context.Books.Include(c =>c.Category).Include(c => c.Ships).ToListAsync();
        }
        //GetById
        public async Task<Books> GetBookById(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(c => c.BookID == id);
            if (book == null) { return null; }
            return book;
        }
        // Add
        public async Task Add(Books book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        // Update 
        public async Task Update(Books book)
        {
            var existingItem = await GetBookById(book.BookID);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(book);
            }
            else
            {
                _context.Books.Add(book);
            }
            await _context.SaveChangesAsync();
        }
        // Delete
        public async Task Delete(int id)
        {
         var book = await GetBookById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

    }
}
