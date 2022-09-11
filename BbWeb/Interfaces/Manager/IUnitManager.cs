using BbWeb.Models;
using EF.Core.Repository.Interface.Manager;

namespace BbWeb.Interfaces.Manager
{
    public interface IUnitManager
    {
        IOrderDetailManager OrderDetail { get; }
        IOrderHeaderManager OrderHeader { get; }
        void Save();
    }
}
