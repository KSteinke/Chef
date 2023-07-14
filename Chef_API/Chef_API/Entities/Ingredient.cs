namespace Chef_API.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Countable { get; set; } 
        public int KCal { get; set; }       //If countable KCal is per unit, if uncountalbe per 100g

        public ICollection<Recipe> Recipes { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
