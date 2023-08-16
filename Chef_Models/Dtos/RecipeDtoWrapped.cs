using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef_Models.Dtos
{
    public class RecipeDtoWrapped
    {
        [Required]
        public string RecipeDtoJson { get; set; }
        [Required]
        public IFormFile RecipeImg { get; set; }
    }
}
