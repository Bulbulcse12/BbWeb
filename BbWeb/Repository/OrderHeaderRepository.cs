using BbWeb.Data;
using BbWeb.Interfaces.Repository;
using BbWeb.Models;
using EF.Core.Repository.Repository;

namespace BbWeb.Repository
{
    public class OrderHeaderRepository : CommonRepository<OrderHeader>, IOrderHeaderRepository
    {
        public OrderHeaderRepository(ApplicationDbContext dbContext) :base(dbContext)
        {

        }
    }
}
