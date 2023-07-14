namespace Chef_API.Entities
{
    public class RecipeIngredient
    {
        public int Quantity { get; set; }


        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        
    }
}
