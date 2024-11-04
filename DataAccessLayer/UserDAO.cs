using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDAO : SingletonBase<UserDAO>
    {
        // Login function
        public async Task<Users> Login(string username, string password)
        {
             var UserLogin = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
            if (UserLogin == null) { return null; }
            return UserLogin;
        }
        //GetAll
        public async Task<IEnumerable<Users>> GetUserAll()
        {
            return await _context.Users.ToListAsync();
        }
        //GetById
        public async Task<Users> GetUserById(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(c => c.UserID == id);
            if (user == null) { return null; }
            return user;
        }
        // Add
        public async Task Add(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        // Update 
        public async Task Update(Users user)
        {
            var existingItem = await GetUserById(user.UserID);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(user);
            }
            else
            {
                _context.Users.Add(user);
            }
            await _context.SaveChangesAsync();
        }
        // Delete
        public async Task Delete(int id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

    }
}
