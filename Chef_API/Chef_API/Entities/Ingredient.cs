namespace Chef_API.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Countable { get; set; } 
        
        public ICollection<Recipe> Recipes { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
