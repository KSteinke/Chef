using Chef_API.Data;
using Chef_API.Entities;
using Chef_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chef_API.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ChefDBContext _chefDBContext;
        public IngredientRepository(ChefDBContext chefDBContext)
        {
            _chefDBContext = chefDBContext;
        }

        /// <summary>
        /// Returns the list of available ingredients
        /// </summary>
        /// <returns></returns>
        public IQueryable<Ingredient> GetIngredients()
        {
            return _chefDBContext.Ingredients.AsQueryable();
        }


        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()    //TODO - Mockup version, in future refactor to return only part of recipes
        {
            return await GetIngredients().ToListAsync();
        }
    }
}
