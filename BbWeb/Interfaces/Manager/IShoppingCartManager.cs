using BbWeb.Models;
using EF.Core.Repository.Interface.Manager;
using System.Linq.Expressions;

namespace BbWeb.Interfaces.Manager
{
    public interface IShoppingCartManager : ICommonManager<ShoppingCart>
    {
        void Save();
        int IncrementCount(ShoppingCart shoppingCart,int count);
        int DecrementCount(ShoppingCart shoppingCart,int count);

        IEnumerable<ShoppingCart> GetAll(Expression<Func<ShoppingCart,bool>>? filter=null,string? includeProperties = null);
    }
}
