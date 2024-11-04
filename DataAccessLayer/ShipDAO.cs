using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ShipDAO : SingletonBase<ShipDAO>
    {
        //GetAllByShipId
        public async Task<Ships> GetShipAllById(int shipId)
        {
            var ship = await _context.Ships
                .Include(s => s.Books)
                .Include(s => s.UsersOrder)
                .Include(s => s.UsersApprove)
                .FirstOrDefaultAsync(s => s.ShipID == shipId);

            return ship;
        }

        //GetAllByUserId
        public async Task<IEnumerable<Ships>> GetShipAllByUserId(int userId)
        {
            var ships = await _context.Ships
                .Include(s => s.Books)
                .Include(s => s.UsersOrder)
                .Include(s => s.UsersApprove)
                .Where(s => s.UserOrderID == userId)
                .ToListAsync();

            return ships;
        }

        //GetAll
        public async Task<IEnumerable<Ships>> GetShipAll()
        {
            var ships = await _context.Ships.Include(s => s.Books).Include(s => s.UsersOrder).Include(s => s.UsersApprove).ToListAsync();
            return ships;
        }
        //GetById
        public async Task<Ships> GetShipById(int id)
        {
            var ship = await _context.Ships.SingleOrDefaultAsync(c => c.ShipID == id);
            if (ship == null) { return null; }
            return ship;
        }
        // Add
        public async Task Add(Ships ship)
        {
            _context.Ships.Add(ship);
            await _context.SaveChangesAsync();
        }
        // Update 
        public async Task Update(Ships ship)
        {
            var existingItem = await GetShipById(ship.ShipID);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(ship);
            }
            else
            {
                _context.Ships.Add(ship);
            }
            await _context.SaveChangesAsync();
        }
        // Delete
        public async Task Delete(int id)
        {
            var ship = await GetShipById(id);
            if (ship != null)
            {
                _context.Ships.Remove(ship);
                await _context.SaveChangesAsync();
            }
        }

    }
}
