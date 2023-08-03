using Chef_API.Data;
using Chef_API.Entities;
using Chef_API.Repositories.Interfaces;
using Chef_API.Services.PasswordMenagement;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chef_API.Repositories
{
    public class ChefRepository : IChefRepository
    {
        private readonly ChefDBContext  _chefDBContext;
        private readonly IConfiguration _config;

       
        public ChefRepository(ChefDBContext chefDBContext, IConfiguration config)
        {
            _chefDBContext = chefDBContext;
            _config = config;
        }

        public async Task<IEnumerable<Chef>> GetChefs()
        {
            var chefs = await _chefDBContext.Chefs.ToListAsync();
            return chefs;
        }

        /// <summary>
        /// Verification of user credentials to login purposes
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns>Bool if user is present in DataBase</returns>
        public async Task<bool> veryfiLoginCredentials(LoginDto userCredentials)
        {
           if(!await _chefDBContext.Chefs.AnyAsync(c => c.UserName == userCredentials.UserName))
           {
                return false;
           }

           var salt = await _chefDBContext.Chefs.Where(c => c.UserName == userCredentials.UserName).Select(s => s.Salt).FirstAsync();
           var passwordEncrypted = PasswordHasher.ComputeHash(userCredentials.Password, salt, _config.GetValue<string>("HashOptions:Pepper"), _config.GetValue<int>("HashOptions:Iterations"));

           if ((from chef in _chefDBContext.Chefs
                     where chef.UserName == userCredentials.UserName
                     select chef.Password).Single() != passwordEncrypted)
           {
                return false;
           }
           return true;
        }

        public async Task<bool> CheckUserExist(LoginDto userCredentials)
        {
            if(await _chefDBContext.Chefs.AnyAsync(c => c.UserName == userCredentials.UserName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Chef> RegisterUser(LoginDto userCredentials)
        {
            var salt = PasswordHasher.GenerateSalt();
            var passwordEncrypted = PasswordHasher.ComputeHash(userCredentials.Password, salt, _config.GetValue<string>("HashOptions:Pepper"), _config.GetValue<int>("HashOptions:Iterations"));
            var newUser = new Chef {
                UserName = userCredentials.UserName,
                Password = passwordEncrypted,
                Salt = salt,
            };
            var result = await _chefDBContext.Chefs.AddAsync(newUser);
            await _chefDBContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
