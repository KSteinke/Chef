using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef_Models.Dtos
{
    public class GetRecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? PrepDescription { get; set; }
        public string Category { get; set; }
        public bool LunchBox { get; set; }
        public string? Diet_Category { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<IngredientDto> IngredientDtos { get; set; }
    }
}
