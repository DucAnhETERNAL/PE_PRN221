using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepositories
{
    public interface IUserRepository
    {
        Task<Users> Login(string username, string password);
        Task<IEnumerable<Users>> GetUserAll();
        Task<Users> GetUserById(int id);
        Task Add(Users user);
        Task Update(Users user);
        Task Delete(int id);
    }
}
