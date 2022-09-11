using BbWeb.Data;
using BbWeb.Interfaces.Manager;
using BbWeb.Models;
using BbWeb.Repository;
using EF.Core.Repository.Manager;
using Microsoft.EntityFrameworkCore;

namespace BbWeb.Manager
{
    public class OrderHeaderManager : CommonManager<OrderHeader>,IOrderHeaderManager
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<OrderHeader> dbset;


        public OrderHeaderManager(ApplicationDbContext dbContext) :base(new OrderHeaderRepository(dbContext))
        {
            _dbContext = dbContext;
            this.dbset = _dbContext.Set<OrderHeader>();
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _dbContext.OrderHeaders.FirstOrDefault(u=>u.Id == id);
            if(orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        void IOrderHeaderManager.Update(OrderHeader obj)
        {
            _dbContext.OrderHeaders.Update(obj);
        }
    }
}
