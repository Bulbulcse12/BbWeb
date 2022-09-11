using BbWeb.Models;
using EF.Core.Repository.Interface.Manager;

namespace BbWeb.Interfaces.Manager
{
    public interface IOrderHeaderManager : ICommonManager<OrderHeader>
    {
        void Update(OrderHeader obj);

        void UpdateStatus(int id,string orderStatus,string? paymentStatus=null);
    }
}
