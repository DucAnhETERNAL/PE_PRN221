using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Books>> GetBookAll();
        Task<Books> GetBookById(int id);
        Task Add(Books book);
        Task Update(Books book);
        Task Delete(int id);
    }
}
