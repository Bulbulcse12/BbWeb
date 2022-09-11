using BbWeb.Data;
using BbWeb.Interfaces.Repository;
using BbWeb.Models;
using EF.Core.Repository.Repository;

namespace BbWeb.Repository
{
    public class OrderDetailRepository : CommonRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ApplicationDbContext dbContext) :base(dbContext)
        {

        }
    }
}
