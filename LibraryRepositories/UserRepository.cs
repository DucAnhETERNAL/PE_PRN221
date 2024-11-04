using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepositories
{
    public class UserRepository: IUserRepository
    {
       //Login
        public async Task<Users> Login(string username, string password)
        {
            return await UserDAO.Instance.Login(username, password);
        }
        // GetAll
        public async Task<IEnumerable<Users>> GetUserAll()
        {
            return await UserDAO.Instance.GetUserAll();
        }

        // GetById
        public async Task<Users> GetUserById(int id)
        {
            return await UserDAO.Instance.GetUserById(id);
        }

        // Add
        public async Task Add(Users user)
        {
            await UserDAO.Instance.Add(user);
        }

        // Update
        public async Task Update(Users user)
        {
            await UserDAO.Instance.Update(user);
        }

        // Delete
        public async Task Delete(int id)
        {
            await UserDAO.Instance.Delete(id);
        }
    }
}
