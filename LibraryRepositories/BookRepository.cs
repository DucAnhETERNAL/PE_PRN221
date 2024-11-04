using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepositories
{
    public class BookRepository:IBookRepository
    {
        public async Task Add(Books book) {
        await BookDAO.Instance.Add(book);
        }
        public async Task<IEnumerable<Books>> GetBookAll()
        {
           return await BookDAO.Instance.GetBookAll();
        }
        public async Task Delete (int id)
        {
            await BookDAO.Instance.Delete(id);
        }
        public async Task<Books> GetBookById(int id)
        {
            return await BookDAO.Instance.GetBookById(id);
        }
        public async Task Update(Books book) {
            await BookDAO.Instance.Update(book);
        }
    }
}
