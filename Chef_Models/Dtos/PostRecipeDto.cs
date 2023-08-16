using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chef_Models.Dtos
{
    public class PostRecipeDto
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [MaxLength(200)]
        public string? PrepDescription { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public bool LunchBox { get; set; }
        [Required]
        public string Diet_Category { get; set; }
        public List<IngredientDto> Ingredients { get; set;}
    }
}
