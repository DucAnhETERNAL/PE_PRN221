using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CategoryDAO : SingletonBase<CategoryDAO>
    {
        //GetAll
        public async Task<IEnumerable<Categories>> GetCategoryAll()
        {
            return await _context.Categories.ToListAsync();
        }
        //GetById
        public async Task<Categories> GetCategoryById(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryID == id);
            if (category == null) { return null; }
            return category;
        }
        // Add
        public async Task Add(Categories category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        // Update 
        public async Task Update(Categories category)
        {
            var existingItem = await GetCategoryById(category.CategoryID);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(category);
            }
            else
            {
                _context.Categories.Add(category);
            }
            await _context.SaveChangesAsync();
        }
        // Delete
        public async Task Delete(int id)
        {
            var category = await GetCategoryById(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

    }
}
