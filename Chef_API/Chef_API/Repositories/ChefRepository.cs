using Chef_API.Data;
using Chef_API.Entities;
using Chef_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chef_API.Repositories
{
    public class ChefRepository : IChefRepository
    {
        private readonly ChefDBContext  _chefDBContext;

        public ChefRepository(ChefDBContext chefDBContext)
        {
            _chefDBContext = chefDBContext;
        }

        public async Task<IEnumerable<Chef>> GetChefs()
        {
            var chefs = await _chefDBContext.Chefs.ToListAsync();
            return chefs;
        }
    }
}
