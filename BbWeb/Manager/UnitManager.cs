using BbWeb.Data;
using BbWeb.Interfaces.Manager;
using BbWeb.Models;
using BbWeb.Repository;
using EF.Core.Repository.Manager;

namespace BbWeb.Manager
{
    public class UnitManager : IUnitManager
    {
        private readonly ApplicationDbContext _db;
        public UnitManager(ApplicationDbContext db)
        {
            _db = db;
            OrderHeader = new OrderHeaderManager(_db);
            OrderDetail = new OrderDetailManager(_db);

        }

        
        public IOrderDetailManager OrderDetail { get; private set; }

        public IOrderHeaderManager OrderHeader { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
