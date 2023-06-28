using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chef_Models.Dtos
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool LunchBox { get; set; }
        public string Diet_Category { get; set; }
        public string RecipePhotoURL { get; set; }
        public string AuthorName { get; set; }
        public string RecipeImgBase64 { get; set; }
    }
}
