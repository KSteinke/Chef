using System.ComponentModel.DataAnnotations.Schema;

namespace Chef_API.Entities
{
    public class Recipe
    {
        
        public int Id { get; set; } 
        public string Name { get; set; }
        [Column(TypeName ="ntext")] //Specification of column type for property below
        public string Description { get; set; }
        [Column(TypeName = "ntext")] //Specification of column type for property below
        public string? PrepDescription { get; set; }
        public string Category { get; set; }
        public bool LunchBox { get; set; }
        public string? Diet_Category { get; set; }
        public string? RecipePhotoURL { get; set; }
        public int AuthorId { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
