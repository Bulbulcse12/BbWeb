using BbWeb.Data;
using BbWeb.Interfaces.Repository;
using BbWeb.Models;
using EF.Core.Repository.Repository;

namespace BbWeb.Repository
{
    public class CategoryRepository : CommonRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) :base(dbContext)
        {

        }
    }
}
