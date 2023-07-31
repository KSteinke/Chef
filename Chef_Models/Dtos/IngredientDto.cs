using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef_Models.Dtos
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Countable { get; set; }
        public int? Quantity { get; set; }
    }
}
