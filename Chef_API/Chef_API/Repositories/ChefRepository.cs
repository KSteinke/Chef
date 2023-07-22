using Chef_API.Data;
using Chef_API.Entities;
using Chef_API.Repositories.Interfaces;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Verification of user credentials to login purposes
        /// </summary>
        /// <param name="chefCredentials"></param>
        /// <returns>Bool if user is present in DataBase</returns>
        public async Task<bool> veryfiLoginCredentials(LoginDto chefCredentials)
        {
           if(!await _chefDBContext.Chefs.AnyAsync(c => c.UserName == chefCredentials.UserName))
           {
                return false;
           }
           else if ((from chef in _chefDBContext.Chefs
                     where chef.UserName == chefCredentials.UserName
                     select chef.Password).Single() != chefCredentials.Password)
           {
                return false;
           }
           return true;
        }
    }
}
