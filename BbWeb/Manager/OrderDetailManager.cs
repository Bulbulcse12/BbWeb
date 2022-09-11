using BbWeb.Data;
using BbWeb.Interfaces.Manager;
using BbWeb.Models;
using BbWeb.Repository;
using EF.Core.Repository.Manager;

namespace BbWeb.Manager
{
    public class OrderDetailManager : CommonManager<OrderDetail>, IOrderDetailManager
    {
        public OrderDetailManager(ApplicationDbContext _dbContext) :base(new OrderDetailRepository(_dbContext))
        {

        }
    }
}
