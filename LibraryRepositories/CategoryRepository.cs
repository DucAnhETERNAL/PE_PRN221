using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepositories
{
    public class CategoryRepository:ICategoryRepository
    {
        public async Task<IEnumerable<Categories>> GetCategoryAll()
        {
            return await CategoryDAO.Instance.GetCategoryAll();
        }

        // GetById
        public async Task<Categories> GetCategoryById(int id)
        {
            return await CategoryDAO.Instance.GetCategoryById(id);
        }

        // Add
        public async Task Add(Categories category)
        {
            await CategoryDAO.Instance.Add(category);
        }

        // Update
        public async Task Update(Categories category)
        {
            await CategoryDAO.Instance.Update(category);
        }

        // Delete
        public async Task Delete(int id)
        {
            await CategoryDAO.Instance.Delete(id);
        }
    }
}
