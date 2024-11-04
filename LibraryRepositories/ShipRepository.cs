using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepositories
{
    public class ShipRepository:IShipRepository
    {
        public async Task<Ships> GetShipAllById(int shipId)
        {
            return await ShipDAO.Instance.GetShipAllById(shipId);
        }
        public async Task<IEnumerable<Ships>> GetShipAllByUserId(int userId)
        {
            return await ShipDAO.Instance.GetShipAllByUserId(userId);
        }
        // GetAll
        public async Task<IEnumerable<Ships>> GetShipAll()
        {
            return await ShipDAO.Instance.GetShipAll();
        }

        // GetById
        public async Task<Ships> GetShipById(int id)
        {
            return await ShipDAO.Instance.GetShipById(id);
        }

        // Add
        public async Task Add(Ships ship)
        {
            await ShipDAO.Instance.Add(ship);
        }

        // Update
        public async Task Update(Ships ship)
        {
            await ShipDAO.Instance.Update(ship);
        }

        // Delete
        public async Task Delete(int id)
        {
            await ShipDAO.Instance.Delete(id);
        }
    }
}
