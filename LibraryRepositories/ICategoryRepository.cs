using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Categories>> GetCategoryAll();
        Task<Categories> GetCategoryById(int id);
        Task Add(Categories category);
        Task Update(Categories category);
        Task Delete(int id);
    }
}
