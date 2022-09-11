using BbWeb.Data;
using BbWeb.Interfaces.Repository;
using BbWeb.Models;
using EF.Core.Repository.Repository;

namespace BbWeb.Repository
{
    public class ShoppingCartRepository : CommonRepository<ShoppingCart>,IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext dbContext) :base(dbContext)
        {

        }
    }
}
