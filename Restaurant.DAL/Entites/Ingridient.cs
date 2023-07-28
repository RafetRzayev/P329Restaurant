using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Entites
{
    public class Ingridient : TimeStample
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<FoodIngridient>? FoodIngridients { get; set; }
    }
}
