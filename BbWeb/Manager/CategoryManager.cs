using BbWeb.Data;
using BbWeb.Interfaces.Manager;
using BbWeb.Models;
using BbWeb.Repository;
using EF.Core.Repository.Manager;

namespace BbWeb.Manager
{
    public class CategoryManager : CommonManager<Category>,ICategoryManager
    {
        public CategoryManager(ApplicationDbContext _dbContext) :base(new CategoryRepository(_dbContext))
        {

        }
    }
}
