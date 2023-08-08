using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chef_Models.Dtos
{
    public class PostRecipeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? PrepDescription { get; set; }
        public string Category { get; set; }
        public bool LunchBox { get; set; }
        public string Diet_Category { get; set; }
        public List<IngredientDto> Ingredients { get; set;}
    }
}
