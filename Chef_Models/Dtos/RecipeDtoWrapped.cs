using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef_Models.Dtos
{
    public class RecipeDtoWrapped
    {
        public string RecipeDtoJson { get; set; }
        public IFormFile RecipeImg { get; set; }
    }
}
