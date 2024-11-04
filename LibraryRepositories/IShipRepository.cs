using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepositories
{
    public interface IShipRepository
    {
        Task<Ships> GetShipAllById(int shipId);
        Task<IEnumerable<Ships>> GetShipAllByUserId(int userId);
        Task<IEnumerable<Ships>> GetShipAll();
        Task<Ships> GetShipById(int id);
        Task Add(Ships ship);
        Task Update(Ships ship);
        Task Delete(int id);
    }
}
