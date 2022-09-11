using BbWeb.Data;
using BbWeb.Interfaces.Manager;
using BbWeb.Models;
using BbWeb.Repository;
using EF.Core.Repository.Manager;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BbWeb.Manager
{
    public class ShoppingCartManager : CommonManager<ShoppingCart>,IShoppingCartManager
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<ShoppingCart> dbset;
       
        public ShoppingCartManager(ApplicationDbContext dbContext) :base(new ShoppingCartRepository(dbContext))
        {
            _dbContext = dbContext;
            this.dbset = _dbContext.Set<ShoppingCart>();
        }

        public int DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        public IEnumerable<ShoppingCart> GetAll(Expression<Func<ShoppingCart, bool>>? filter=null, string? includeProperties = null)
        {
            IQueryable<ShoppingCart> query = dbset;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            
            if(includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] {','}))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        public int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
